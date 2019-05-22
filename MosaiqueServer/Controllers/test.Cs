using System;
using System.Runtime.InteropServices;

namespace MosaiqueServeur.Controllers
{
    public class test
    {



        

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent,
            IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        public static void hidebutton_Click()
        {
            IntPtr hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            ShowWindow(hWnd, 0);
        }
        public static void showbutton_Click()
        {
            IntPtr hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            ShowWindow(hWnd, 5);
        }








        /// <summary>
        /// taskbar
        /// </summary>
        /// <param name="className"></param>
        /// <param name="windowText"></param>
        /// <returns></returns>
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

        protected static int Handle
        {
            get
            {
                return FindWindow("Shell_TrayWnd", "");
            }
        }

        protected static int HandleOfStartButton
        {
            get
            {
                int handleOfDesktop = GetDesktopWindow();
                int handleOfStartButton = FindWindowEx(handleOfDesktop, 0, "button", 0);
                return handleOfStartButton;
            }
        }

        private test()
        {
            // hide ctor
        }

        public static void Show()
        {
            ShowWindow(Handle, SW_SHOW);
            ShowWindow(HandleOfStartButton, SW_SHOW);
        }

        public static void Hide()
        {
            ShowWindow(Handle, SW_HIDE);
            ShowWindow(HandleOfStartButton, SW_HIDE);
        }
    }

    public class testclock

    {

        IntPtr shelltraywnd;
        IntPtr traynotifywnd;
        IntPtr trayclockwclass;
        public testclock()
        {
            
            shelltraywnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "shell_traywnd", string.Empty);
            traynotifywnd = FindWindowEx(shelltraywnd, IntPtr.Zero, "traynotifywnd", string.Empty);
            trayclockwclass = FindWindowEx(traynotifywnd, IntPtr.Zero, "trayclockwclass", string.Empty);
        }
        [DllImport("User32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("USER32.DLL")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        int WM_PAINT = 15;
        public void button1_Click()
        {
            ShowWindow(trayclockwclass, 0);
        }

        public void button2_Click()
        {
            ShowWindow(trayclockwclass, 1);
        }

        public void button3_Click()
        {
            SendMessage(traynotifywnd, WM_PAINT, IntPtr.Zero, IntPtr.Zero);
        }

    }




}
