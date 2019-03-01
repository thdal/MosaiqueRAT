using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoDownloadFile : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoDownloadFile;
            }
        }

        [Index(0)]
        public virtual string remotePath { get; set; }

        [Index(1)]
        public virtual int id { get; set; }

        [Index(2)]
        public virtual int lvItem { get; set; }

        public DoDownloadFile()
        {
        }

        public DoDownloadFile(string remotePath, int id, int lvItem)
        {
            this.remotePath = remotePath;
            this.id = id;
            this.lvItem = lvItem;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
