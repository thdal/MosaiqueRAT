using Microsoft.Win32;
using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class DoCreateRegistryValue : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.DoCreateRegistryValue;
            }
        }

        [Index(0)]
        public virtual string keyPath { get; set; }

        [Index(1)]
        public virtual RegistryValueKind kind { get; set; }

        public DoCreateRegistryValue()
        {
        }

        public DoCreateRegistryValue(string keyPath, RegistryValueKind kind)
        {
            this.keyPath = keyPath;
            this.kind = kind;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
