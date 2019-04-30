using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetAvailableWebcams : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetAvailableWebcams;
            }
        }

        public GetAvailableWebcams()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
