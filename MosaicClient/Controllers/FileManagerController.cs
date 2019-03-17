using Client.Controllers.Tools;
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
    public static partial class FileManagerController
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
                        {
                            break;
                        }

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

        public static void doDownloadFileCancel(DoDownloadFileCancel packet, ClientMosaic client)
        {
            if (!_canceledDownloads.ContainsKey(packet.id))
            {
                _canceledDownloads.Add(packet.id, "canceled");
                new DoDownloadFileResponse(packet.id, "canceled", new byte[0], -1, -1, "Canceled").Execute(client);
            }
        }
    }
}
