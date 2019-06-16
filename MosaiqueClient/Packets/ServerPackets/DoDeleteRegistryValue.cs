using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoDeleteRegistryValue : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoDeleteRegistryValue;
            }
        }

        [Index(0)]
        public virtual string keyPath { get; set; }

        [Index(1)]
        public virtual string valueName { get; set; }

        public DoDeleteRegistryValue()
        {
        }

        public DoDeleteRegistryValue(string keyPath, string valueName)
        {
            this.keyPath = keyPath;
            this.valueName = valueName;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
