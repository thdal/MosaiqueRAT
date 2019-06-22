using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class ActiveSession : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.ActiveSession;
            }
        }

        [Index(0)]
        public virtual int sessionInt { get; set; }

        public ActiveSession()
        {
        }

        public ActiveSession(int sessionInt)
        {
            this.sessionInt = sessionInt;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
