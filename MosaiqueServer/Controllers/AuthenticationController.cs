using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Serveur.Controllers
{
    //Authentification
    class AuthenticationController
    {
        private static readonly char[] _illegalChars = Path.GetInvalidPathChars().Union(Path.GetInvalidFileNameChars()).ToArray();

        public static void HandleGetAuthenticationResponse(ClientMosaique client, GetAuthenticationResponse packet)
        {
            if (client.endPoint.Address.ToString() == "255.255.255.255")
                return;

            try
            {
                client.value.version = packet.version;
                client.value.operatingSystem = packet.operatingSystem;
                client.value.accountType = packet.accountType;
                client.value.country = packet.country;
                client.value.countryCode = packet.countryCode;
                client.value.city = packet.city;
                client.value.imageIndex = packet.imageIndex;
                client.value.id = packet.id;
                client.value.name = packet.name;
                client.value.clientIdentifier = packet.clientID;

                client.value.downloadDirectory = (checkPathForIllegalChars(client.value.name)) ?
                    Path.Combine(Application.StartupPath, string.Format("Clients\\{0}_{1}\\", client.value.name, client.value.id.Substring(0, 7))) :
                    Path.Combine(Application.StartupPath, string.Format("Clients\\{0}_{1}\\", client.endPoint.Address, client.value.id.Substring(0, 7)));
            }
            catch
            {
            }
        }

    public static bool checkPathForIllegalChars(string path)
    {
        return path.Any(c => _illegalChars.Contains(c));
    }

    }
}
