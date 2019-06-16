using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoCreateRegistryKey : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoCreateRegistryKey;
            }
        }

        [Index(0)]
        public virtual string parentPath { get; set; }

        public DoCreateRegistryKey()
        {
        }

        public DoCreateRegistryKey(string parentPath)
        {
            this.parentPath = parentPath;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
