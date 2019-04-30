using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class UninstallClient :IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.UninstallClient;
            }
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
