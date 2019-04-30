using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
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

        public DoDownloadFile()
        {
        }

        public DoDownloadFile(string remotePath, int id)
        {
            this.remotePath = remotePath;
            this.id = id;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
