using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class MsgToRemoteChat : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.MsgToRemoteChat;
            }
        }

        [Index(0)]
        public virtual string message { get; set; }

        public MsgToRemoteChat()
        {
        }

        public MsgToRemoteChat(string message)
        {
            this.message = message;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
