using MosaiqueServeur.Packets.ServerPackets;
using Serveur.Controllers.Server;
using System;
using System.Drawing;

namespace Serveur.Controllers
{
    static class FrmRemoteChatController
    {
        public static void msgFromRemoteChat( ClientMosaique client, MsgToRemoteChat packet)
        {
            if(client.value.frmRChat == null)
                return;

            client.value.frmRChat.updateRTxtChat("[ " + DateTime.Now.ToShortTimeString() + " ]", Color.Red);
            client.value.frmRChat.updateRTxtChat(' ' + packet.message + Environment.NewLine, Color.Black);
        }
    }
}
