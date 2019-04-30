using Client.Packets.ServerPackets;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Client.Controllers
{
    static class FrmRemoteChatController
    {
        public static void msgFromRemoteChat(MsgToRemoteChat packet, ClientMosaique client)
        {
            if (client.frmRChat == null)
            {
                client.frmRChat = new Views.FrmRemoteChat(client);
                new Thread(() => client.frmRChat.ShowDialog()).Start();
                client.frmRChat.BringToFront();
                client.frmRChat.Focus();
                Thread.Sleep(2000);
            }
            client.frmRChat.updateRTxtChat("[ " + DateTime.Now.ToShortTimeString() + " ]", Color.Red);
            client.frmRChat.updateRTxtChat(' ' + packet.message + Environment.NewLine, Color.Black);
        }

        public static void closeRemoteChat(ClientMosaique client)
        {
            if (client.frmRChat != null)
            {
                client.frmRChat.Close();
                client.frmRChat = null;
            }
        }
    }
}
