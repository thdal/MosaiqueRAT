using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoRenameRegistryKey : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoRenameRegistryKey;
            }
        }

        [Index(0)]
        public virtual string ParentPath { get; set; }

        [Index(1)]
        public virtual string OldKeyName { get; set; }

        [Index(2)]
        public virtual string NewKeyName { get; set; }

        public DoRenameRegistryKey()
        {
        }

        public DoRenameRegistryKey(string parentPath, string oldKeyName, string newKeyName)
        {
            ParentPath = parentPath;
            OldKeyName = oldKeyName;
            NewKeyName = newKeyName;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
