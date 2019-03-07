using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoStartupItemRemove : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoStartupItemRemove;
            }
        }

        [Index(0)]
        public virtual string name { get; set; }

        [Index(0)]
        public virtual string path { get; set; }

        [Index(0)]
        public virtual int type { get; set; }

        public DoStartupItemRemove()
        {
        }

        public DoStartupItemRemove(string name, string path, int type)
        {
            this.name = name;
            this.path = path;
            this.type = type;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
