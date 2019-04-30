using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaicServeur.Packets.ServerPackets
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
