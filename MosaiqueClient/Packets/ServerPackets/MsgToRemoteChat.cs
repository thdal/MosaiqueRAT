using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
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
