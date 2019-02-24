using Serveur.Controllers.Server;
using Serveur.Models;
using Serveur.Packets.ClientPackets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Serveur.Controllers
{
    public static class FrmFileManagerController
    {
        private const string DELIMITER = "$E$";
        private static readonly string[] _sizes = { "B", "KB", "MB", "GB" };
        private static readonly char[] _illegalChars = Path.GetInvalidPathChars().Union(Path.GetInvalidFileNameChars()).ToArray();
        public static Dictionary<int, string> canceledDownloads = new Dictionary<int, string>();
        public static Dictionary<int, string> renamedFiles = new Dictionary<int, string>();


        public enum PathType
        {
            File,
            Directory,
            Back
        }

        public static void getDrivesResponse(ClientMosaic client, GetDrivesResponse packet)
        {
            if (client.value == null || client.value.frmFm == null || packet.driveDisplayName == null || packet.rootDirectory == null)
                return;

            if (packet.driveDisplayName.Length != packet.rootDirectory.Length) return;

            RemoteDrive[] drives = new RemoteDrive[packet.driveDisplayName.Length];
            for (int i = 0; i < packet.driveDisplayName.Length; i++)
            {
                drives[i] = new RemoteDrive(packet.driveDisplayName[i], packet.rootDirectory[i]);
            }

            if (client.value != null && client.value.frmFm != null)
            {
                client.value.frmFm.addDrives(drives);
                client.value.frmFm.setStatus("Ready");
            }
        }

        public static void getDirectoryResponse(ClientMosaic client, GetDirectoryResponse packet)
        {
            if(client.value == null || client.value.frmFm == null)
            {
                return;
            }

            new Thread(() =>
            {
                if (client.value.processingDirectory) return;
                client.value.processingDirectory = true;

                client.value.frmFm.clearFileBrowser();
                client.value.frmFm.addItemToFileBrowser("..", "", PathType.Back, 0);

                if (packet.folders != null && packet.folders.Length != 0 && client.value.processingDirectory)
                {
                    for (int i = 0; i < packet.folders.Length; i++)
                    {
                        if (packet.folders[i] != DELIMITER)
                        {
                            if (client.value == null || client.value.frmFm == null || !client.value.processingDirectory)
                                break;

                            client.value.frmFm.addItemToFileBrowser(packet.folders[i], "", PathType.Directory, 1);
                        }
                    }
                }

                if (packet.files != null && packet.files.Length != 0 && client.value.processingDirectory)
                {
                    for (int i = 0; i < packet.files.Length; i++)
                    {
                        if (packet.files[i] != DELIMITER)
                        {
                            if (client.value == null || client.value.frmFm == null || !client.value.processingDirectory)
                                break;

                            client.value.frmFm.addItemToFileBrowser(packet.files[i], getDataSize(packet.filesSize[i]), PathType.File, 4);
                        }
                    }
                }

                if (client.value != null)
                {
                    client.value.receivedLastDirectory = true;
                    client.value.processingDirectory = false;
                    if (client.value.frmFm != null)
                    {
                        client.value.frmFm.setStatus("Ready");
                    }
                }
            }).Start();
        }

        public static void doDownloadFileResponse(ClientMosaic client, DoDownloadFileResponse packet)
        {
            if (canceledDownloads.ContainsKey(packet.id) || string.IsNullOrEmpty(packet.fileName))
                return;

            // don't escape from download directory
            if (checkPathForIllegalChars(packet.fileName))
            {
                // disconnect malicious client
                client.disconnect();
                return;
            }

            if (!Directory.Exists(client.value.downloadDirectory))
                Directory.CreateDirectory(client.value.downloadDirectory);

            string downloadPath = Path.Combine(client.value.downloadDirectory, packet.fileName);

            if (packet.currentBlock == 0 && File.Exists(downloadPath))
            {
                for (int i = 1; i < 100; i++)
                {
                    var newFileName = string.Format("{0} ({1}){2}", Path.GetFileNameWithoutExtension(downloadPath), i, Path.GetExtension(downloadPath));
                    if (File.Exists(Path.Combine(client.value.downloadDirectory, newFileName))) continue;

                    downloadPath = Path.Combine(client.value.downloadDirectory, newFileName);
                    renamedFiles.Add(packet.id, newFileName);
                    break;
                }
            }
            else if (packet.currentBlock > 0 && File.Exists(downloadPath) && renamedFiles.ContainsKey(packet.id))
            {
                downloadPath = Path.Combine(client.value.downloadDirectory, renamedFiles[packet.id]);
            }

            if (client.value == null || client.value.frmFm == null)
            {
                //FrmMain.Instance.SetStatusByClient(client, "Download aborted, please keep the File Manager open.");
                //new Packets.ServerPackets.DoDownloadFileCancel(packet.id).Execute(client);
                return;
            }

            int index = client.value.frmFm.getTransferIndex(packet.id);
            if (index < 0)
                return;

            if (!string.IsNullOrEmpty(packet.customMessage))
            {
                if (client.value.frmFm == null) // abort download when form is closed
                    return;

                client.value.frmFm.updateTransferStatus(index, packet.customMessage, 0);
                return;
            }

            FileSplit destFile = new FileSplit(downloadPath);
            if (!destFile.AppendBlock(packet.block, packet.currentBlock))
            {
                if (client.value == null || client.value.frmFm == null)
                    return;

                client.value.frmFm.updateTransferStatus(index, destFile.LastError, 0);
                return;
            }

            decimal progress =
                Math.Round((decimal)((double)(packet.currentBlock + 1) / (double)packet.maxBlocks * 100.0), 2);

            if (client.value == null || client.value.frmFm == null)
                return;

            if (canceledDownloads.ContainsKey(packet.id)) return;

            client.value.frmFm.updateTransferStatus(index, string.Format("Downloading...({0}%)", progress), -1);

            if ((packet.currentBlock + 1) == packet.maxBlocks)
            {
                if (client.value.frmFm == null)
                    return;
                renamedFiles.Remove(packet.id);
                client.value.frmFm.updateTransferStatus(index, "Completed", 1);
            }
        }

        public static bool checkPathForIllegalChars(string path)
        {
            return path.Any(c => _illegalChars.Contains(c));
        }

        public static string getDataSize(long size)
        {
            double len = size;
            int order = 0;
            while (len >= 1024 && order + 1 < _sizes.Length)
            {
                order++;
                len = len / 1024;
            }
            return string.Format("{0:0.##} {1}", len, _sizes[order]);
        }

        public static void setStatusFileManager(ClientMosaic client, SetStatusFileManager packet)
        {
            if (client.value == null || client.value.frmFm == null) return;

            client.value.frmFm.setStatus(packet.message, packet.setLastDirSeen);
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
