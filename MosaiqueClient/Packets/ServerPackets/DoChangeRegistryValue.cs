using Client.Controllers;
using Client.Models;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoChangeRegistryValue : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoChangeRegistryValue;
            }
        }

        [Index(0)]
        public virtual string KeyPath { get; set; }

        [Index(1)]
        public virtual RegValueData Value { get; set; }


        public DoChangeRegistryValue()
        {
        }

        public DoChangeRegistryValue(string keyPath, RegValueData value)
        {
            KeyPath = keyPath;
            Value = value;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
