using Client.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetDesktopResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDesktopResponse;
            }
        }

        [Index(0)]
        public virtual byte[] image { get; set; }

        public GetDesktopResponse()
        {
        }

        public GetDesktopResponse(byte[] image)
        {
            this.image = image;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
