using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
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

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
