using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetDrives : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDrives;
            }
        }
        public GetDrives()
        {
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
