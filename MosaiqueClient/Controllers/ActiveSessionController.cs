using Client.Packets.ServerPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public static class ActiveSessionController
    {
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

        [DllImport("user32")]
        public static extern void LockWorkStation();

        public static void logOff()
        {
            ExitWindowsEx(0, 0);
        }

        public static void lockCmptr()
        {
            LockWorkStation();
        }

        public static void sessionButton(ActiveSession packet)
        {
            try
            {
                if (packet.sessionInt == 1)
                {
                    logOff();
                }
                else
                {
                    lockCmptr();
                }
            }
            catch
            {

            }
           
        }
    }
}
