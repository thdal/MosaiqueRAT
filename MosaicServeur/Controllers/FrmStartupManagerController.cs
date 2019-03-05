using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using System;
using System.Windows.Forms;

namespace Serveur.Controllers
{
    public static class FrmStartupManagerController
    {
        public static void getStartupItemsResponse(ClientMosaic client, GetStartupItemsResponse packet)
        {
            if (client.value == null || client.value.frmStm == null || packet.startupItems == null)
                return;

            foreach(var item in packet.startupItems)
            {
                if (client.value == null || client.value.frmStm == null) return;

                int type;

                if (!int.TryParse(item.Substring(0, 1), out type)) continue;

                string preparedItem = item.Remove(0, 1);
                var temp = preparedItem.Split(new string[] { "||" }, StringSplitOptions.None);

                var l = new ListViewItem(temp)
                {
                    Group = client.value.frmStm.getGroup(type),
                    Tag = type
                };

                if (l.Group == null)
                    return;

                client.value.frmStm.addAutostartItemToListview(l);
            }
        }
    }
}
