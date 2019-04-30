
using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{

    [ZeroFormattable]
    public class CloseRemoteChat : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.CloseRemoteChat;
            }
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
