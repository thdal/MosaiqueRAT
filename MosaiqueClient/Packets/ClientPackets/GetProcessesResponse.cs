using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetProcessesResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetProcessesResponse;
            }
        }

        [Index(0)]
        public virtual string[] pNames { get; set; }

        [Index(1)]
        public virtual int[] pIds { get; set; }

        [Index(2)]
        public virtual string[] pTitles { get; set; }

        public GetProcessesResponse()
        {
        }

        public GetProcessesResponse(string[] pNames, int[] pIds, string[] pTitles)
        {
            this.pNames = pNames;
            this.pIds = pIds;
            this.pTitles = pTitles;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
