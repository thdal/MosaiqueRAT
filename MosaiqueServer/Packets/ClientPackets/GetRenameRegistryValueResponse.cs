using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetRenameRegistryValueResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetRenameRegistryValueResponse;
            }
        }

        [Index(0)]
        public virtual string KeyPath { get; set; }

        [Index(1)]
        public virtual string OldValueName { get; set; }

        [Index(2)]
        public virtual string NewValueName { get; set; }

        [Index(3)]
        public virtual bool IsError { get; set; }

        [Index(4)]
        public virtual string ErrorMsg { get; set; }

        public GetRenameRegistryValueResponse()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
