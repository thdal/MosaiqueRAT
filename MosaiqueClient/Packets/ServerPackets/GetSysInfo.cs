using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetSysInfo : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetSysInfo;
            }
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
