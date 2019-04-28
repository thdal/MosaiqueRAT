using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class SetStatusFileManager : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.SetStatusFileManager;
            }
        }
        [Index(0)]
        public virtual string message { get; set; }

        [Index(1)]
        public virtual bool setLastDirSeen { get; set; }

        public SetStatusFileManager()
        {
        }

        public SetStatusFileManager(string message, bool setLastDirSeen)
        {
            this.message = message;
            this.setLastDirSeen = setLastDirSeen;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
