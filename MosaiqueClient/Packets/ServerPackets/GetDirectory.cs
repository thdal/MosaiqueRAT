using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetDirectory : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDirectory;
            }
        }

        [Index(0)]
        public virtual string remotePath { get; set; }

        public GetDirectory()
        {
        }

        public GetDirectory(string remotePath)
        {
            this.remotePath = remotePath;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
