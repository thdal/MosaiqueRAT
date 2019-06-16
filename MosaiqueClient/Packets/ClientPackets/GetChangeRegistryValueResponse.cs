using Client.Controllers;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetChangeRegistryValueResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetChangeRegistryValueResponse;
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

        public GetChangeRegistryValueResponse()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
