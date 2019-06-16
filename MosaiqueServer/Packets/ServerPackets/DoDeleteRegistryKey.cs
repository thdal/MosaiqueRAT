using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoDeleteRegistryKey : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoDeleteRegistryKey;
            }
        }

        [Index(0)]
        public virtual string parentPath { get; set; }

        [Index(1)]
        public virtual string keyName { get; set; }

        public DoDeleteRegistryKey()
        {
        }

        public DoDeleteRegistryKey(string parentPath, string keyName)
        {
            this.parentPath = parentPath;
            this.keyName = keyName;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
