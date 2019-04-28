using Serveur.Controllers.Server;
using System;
using ZeroFormatter;

namespace Serveur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class SetAuthenticationSuccess : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.SetAuthenticationSuccess;
            }
        }

        public void Execute(ClientMosaic client)
        {
            client.send(this);
        }
    }
}
