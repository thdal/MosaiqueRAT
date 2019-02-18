using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using Serveur.Packets.ServerPackets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Controllers
{
    public class FrmRemoteWebcamController
    {
        //Propriétés
        public ClientMosaic client;
        public static string[] webcams;

        public FrmRemoteWebcamController(ClientMosaic client)
        {
            this.client = client;
            new GetAvailableWebcams().Execute(client);
        }

        public void startFlux(int selectedWebcam)
        {
            //client.value.FrmWbc.picWebcam.Start();
            //client.value.FrmWbc.picWebcam.SetFrameUpdatedEvent(_frameCounter_FrameUpdated);
            //client.value.FrmWbc.ActiveControl = _connectClient.Value.FrmWbc.picWebcam;
            new GetWebcam(75, selectedWebcam).Execute(client);
        }

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
