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

        public GetDesktopResponse()
        {
        }

        public GetDesktopResponse(byte[] image)
        {
            this.image = image;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
