using Client.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetRenameRegistryKeyResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetRenameRegistryKeyResponse;
            }
        }

        [Index(0)]
        public virtual string ParentPath { get; set; }

        [Index(1)]
        public virtual string OldKeyName { get; set; }

        [Index(2)]
        public virtual string NewKeyName { get; set; }

        [Index(3)]
        public virtual bool IsError { get; set; }

        [Index(4)]
        public virtual string ErrorMsg { get; set; }

        public GetRenameRegistryKeyResponse()
        {
        }

        public GetRenameRegistryKeyResponse(string parentPath, string oldKeyName, string newKeyName)
        {
            ParentPath = parentPath;
            OldKeyName = oldKeyName;
            NewKeyName = newKeyName;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
