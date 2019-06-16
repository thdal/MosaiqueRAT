using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetDeleteRegistryValueResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDeleteRegistryValueResponse;
            }
        }

        [Index(0)]
        public virtual string KeyPath { get; set; }

        [Index(1)]
        public virtual string ValueName { get; set; }

        [Index(2)]
        public virtual bool IsError { get; set; }

        [Index(3)]
        public virtual string ErrorMsg { get; set; }

        public GetDeleteRegistryValueResponse()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
