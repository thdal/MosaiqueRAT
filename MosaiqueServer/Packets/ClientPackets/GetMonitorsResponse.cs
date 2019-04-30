using Serveur.Controllers.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetMonitorsResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetMonitorsResponse;
            }
        }

        [Index(0)]
        public virtual int number { get; set; }

        public GetMonitorsResponse()
        {
        }

        public GetMonitorsResponse(int number)
        {
            this.number = number;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
