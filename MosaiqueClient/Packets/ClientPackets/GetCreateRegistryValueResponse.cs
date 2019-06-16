using Client.Controllers;
using Client.Models;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetCreateRegistryValueResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetCreateRegistryValueResponse;
            }
        }

        [Index(0)]
        public virtual string KeyPath { get; set; }

        [Index(1)]
        public virtual RegValueData Value { get; set; }

        [Index(2)]
        public virtual bool IsError { get; set; }

        [Index(3)]
        public virtual string ErrorMsg { get; set; }

        public GetCreateRegistryValueResponse()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
