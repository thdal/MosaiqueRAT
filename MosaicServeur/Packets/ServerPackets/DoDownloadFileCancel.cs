using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
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

        [Index(1)]
        public virtual int lvItem { get; set; }

        public DoDownloadFileCancel()
        {
        }

        public DoDownloadFileCancel(int id, int lvItem)
        {
            this.id = id;
            this.lvItem = lvItem;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
