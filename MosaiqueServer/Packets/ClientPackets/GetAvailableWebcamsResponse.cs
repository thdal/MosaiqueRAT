using Serveur.Controllers.Server;
using System.Collections.Generic;
using System.Drawing;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetAvailableWebcamsResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetAvailableWebcamsResponse;
            }
        }

        [Index(0)]
        public virtual Dictionary<string, List<string>> webcams { get; set; }

        public GetAvailableWebcamsResponse()
        {
        }

        public GetAvailableWebcamsResponse(Dictionary<string, List<string>> webcams)
        {
            this.webcams = webcams;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
