using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using System.Threading;

namespace Serveur.Controllers
{
    public static class FrmTaskManagerController
    {
        public static void getProcessesResponse(ClientMosaique client, GetProcessesResponse packet)
        {
            if (client.value == null || client.value.frmTm == null)
                return;

            client.value.frmTm.clearListViewItems();

            if (packet.pNames == null || packet.pIds == null || packet.pTitles == null ||
                packet.pNames.Length != packet.pIds.Length || packet.pNames.Length != packet.pTitles.Length)
                return;

            new Thread(() =>
            {
                for(int i = 0; i < packet.pNames.Length; i++)
                {
                    if (packet.pIds[i] == 0 || packet.pNames[i] == "System.exe")
                        continue;

                    if (client.value == null || client.value.frmTm == null)
                        break;

                    client.value.frmTm.addProcessesToListView(packet.pNames[i], packet.pIds[i], packet.pTitles[i]);
                }

            }).Start();
        }
    }
}
