using Client.Controllers;
using System.Windows.Forms;

namespace Client.Packets
{
    public static class PacketHandler
    {
        public static void packetChecker(ClientMosaic client, IPackets packet)
        {
            var type = packet.Type;

            if (type == TypePackets.GetMonitors)
            {
                RemoteDesktopController.getMonitors(client);
            }
            else if(type == TypePackets.GetDesktop)
            {
                RemoteDesktopController.getDesktop(client);
            }
        }
    }
}
