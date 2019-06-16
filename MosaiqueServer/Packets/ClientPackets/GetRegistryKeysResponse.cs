using Microsoft.Win32;
using MosaiqueServeur.Models;
using Serveur.Controllers.Server;
using Serveur.Packets;
using System.Collections.Generic;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetRegistryKeysResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetRegistryKeysResponse;
            }
        }

        [Index(0)]
        public virtual RegSeekerMatch[] matches { get; set; }

        [Index(1)]
        public virtual string rootKey { get; set; }

        [Index(2)]
        public virtual bool isError { get; set; }

        [Index(3)]
        public virtual string errorMsg { get; set; }

        public GetRegistryKeysResponse()
        {
        }

        public GetRegistryKeysResponse(RegSeekerMatch[] m, string r, bool e, string eM)
        {
            this.matches = m;
            this.rootKey = r;
            this.isError = e;
            this.errorMsg = eM;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
