using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetMonitors : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetMonitors;
            }
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
