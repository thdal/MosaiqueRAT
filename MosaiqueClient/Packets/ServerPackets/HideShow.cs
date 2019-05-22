using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
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
