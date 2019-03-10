using System.Windows.Forms;

namespace Client.Models
{
    public static class ClientData
    {
        public static string currentPath { get; set; }
        public static string installPath { get; set; }
        public static string AddToStartupFailed { get; set; }

        static ClientData()
        {
            currentPath = Application.ExecutablePath;
        }
    }
}
