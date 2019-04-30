using Client.Packets.ServerPackets;
using System.Windows.Forms;

namespace Client.Controllers
{
    static class SendMessageBoxController
    {
        public static MessageBoxIcon icon;

        public static void MessageBoxShow(SendMessageBox packet)
        {
            selectIcon(packet.icon);
            MessageBox.Show(packet.message, packet.title, MessageBoxButtons.OK,
              icon, MessageBoxDefaultButton.Button1,
              MessageBoxOptions.DefaultDesktopOnly);
        }

        public static void selectIcon(int i)
        {
            switch (i)
            {
                case 1:
                    icon = MessageBoxIcon.Information;
                    break;
                case 2:
                    icon = MessageBoxIcon.Question;
                    break;
                case 3:
                    icon = MessageBoxIcon.Error;
                    break;
                case 4:
                    icon = MessageBoxIcon.Warning;
                    break;
                case 5:
                    icon = MessageBoxIcon.None;
                    break;
            }
        }
    }
}
