using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoDownloadFileCancel : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoDownloadFileCancel;
            }
        }

        [Index(0)]
        public virtual int id { get; set; }

        public DoDownloadFileCancel()
        {
        }

        public DoDownloadFileCancel(int id)
        {
            this.id = id;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
