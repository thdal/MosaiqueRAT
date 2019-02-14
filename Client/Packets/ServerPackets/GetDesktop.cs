using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
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
        public virtual int Quality { get; set; }

        [Index(1)]
        public virtual int Monitor { get; set; }

        public GetDesktop()
        {
        }

        public GetDesktop(int quality, int monitor)
        {
            this.Quality = quality;
            this.Monitor = monitor;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
