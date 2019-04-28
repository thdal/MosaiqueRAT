using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoAskElevate : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoAskElevate;
            }
        }

        public void Execute(ClientMosaic client)
        {

        }
    }
}
