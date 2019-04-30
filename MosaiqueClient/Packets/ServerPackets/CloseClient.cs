using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class CloseClient : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.CloseClient;
            }
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
