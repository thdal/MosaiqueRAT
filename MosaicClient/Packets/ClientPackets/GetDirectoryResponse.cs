using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetDirectoryResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDirectoryResponse;
            }
        }

        [Index(0)]
        public virtual string[] files { get; set; }

        [Index(1)]
        public virtual string[] folders { get; set; }

        [Index(2)]
        public virtual long[] filesSize { get; set; }

        public GetDirectoryResponse()
        {
        }

        public GetDirectoryResponse(string[] files, string[] folders, long[] filesSize)
        {
            this.files = files;
            this.folders = folders;
            this.filesSize = filesSize;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
