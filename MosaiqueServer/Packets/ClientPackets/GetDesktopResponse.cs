using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetDesktopResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDesktopResponse;
            }
        }

        [Index(0)]
        public virtual byte[] image { get; set; }

        [Index(1)]
        public virtual int monitor { get; set; }

        public GetDesktopResponse()
        {
        }

        public GetDesktopResponse(byte[] image, int monitor)
        {
            this.image = image;
            this.monitor = monitor;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
