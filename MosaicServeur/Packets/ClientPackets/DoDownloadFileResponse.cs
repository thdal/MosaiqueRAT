using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class DoDownloadFileResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoDownloadFileResponse;
            }
        }

        [Index(0)]
        public virtual int id { get; set; }

        [Index(1)]
        public virtual string fileName { get; set; }

        [Index(2)]
        public virtual byte[] block { get; set; }

        [Index(3)]
        public virtual int maxBlocks { get; set; }

        [Index(4)]
        public virtual int currentBlock { get; set; }

        [Index(5)]
        public virtual string customMessage { get; set; }

        public DoDownloadFileResponse()
        {
        }

        public DoDownloadFileResponse(int id, string fileName, byte[] block, int maxBlocks, int currentBlock, string customMessage)
        {
            this.id = id;
            this.fileName = fileName;
            this.block = block;
            this.maxBlocks = maxBlocks;
            this.currentBlock = currentBlock;
            this.customMessage = customMessage;
        }

        public void Execute(ClientMosaic client)
        {
            client.sendBlocking(this);
        }
    }
}
