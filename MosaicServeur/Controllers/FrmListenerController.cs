using Serveur.Controllers.Server;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Serveur.Controllers
{
    public class FrmListenerController
    {
        private Socket _serverSocket;

        private const int BUFFER_SIZE = 2048;
        //private const int PORT = 4444;
        private int _port;
        private static readonly byte[] _buffer = new byte[BUFFER_SIZE];
        private static readonly string SettingsPath = Path.Combine(Application.StartupPath, "settings.xml");

        public void listen(int port)
        {
            _port = port;

            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                _serverSocket.Listen(1000);               

                _serverSocket.BeginAccept(new AsyncCallback(acceptClient), null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void stopListening()
        {
            if(_serverSocket != null)
            {
                _serverSocket.Dispose();
                _serverSocket.Close();
                _serverSocket = null;
            }
        }

        private void acceptClient(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = _serverSocket.EndAccept(AR);
            }
            catch (Exception)
            {
                return;
            }

            authentication(new ClientMosaic(socket));
            _serverSocket.BeginAccept(acceptClient, null);
        }

        private void authentication(ClientMosaic client)
        {
            new Packets.ServerPackets.GetAuthentication().Execute(client); // begin handshake                   
        }

        #region XML PART

        public static int listenPort
        {
            get
            {
                return int.Parse(readValueSafe("listenPort", "4444"));
            }
            set
            {
                writeValue("listenPort", value.ToString());
            }
        }
        public static bool autoListen
        {
            get
            {
                return bool.Parse(readValueSafe("autoListen", "false"));
            }
            set
            {
                writeValue("autoListen", value.ToString());
            }
        }
        public static bool startListen
        {
            get
            {
                return bool.Parse(readValueSafe("startListen", "false"));
            }
            set
            {
                writeValue("startListen", value.ToString());
            }
        }
        public static bool showPopup
        {
            get
            {
                return bool.Parse(readValueSafe("showPopup", "false"));
            }
            set
            {
                writeValue("showPopup", value.ToString());
            }
        }
        private static string readValue(string pstrValueToRead)
        {
            try
            {
                XPathDocument doc = new XPathDocument(SettingsPath);
                XPathNavigator nav = doc.CreateNavigator();
                XPathExpression expr = nav.Compile(@"/settings/" + pstrValueToRead);
                XPathNodeIterator iterator = nav.Select(expr);
                while (iterator.MoveNext())
                {
                    return iterator.Current.Value;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        private static string readValueSafe(string pstrValueToRead, string defaultValue = "")
        {
            string value = readValue(pstrValueToRead);
            return (!string.IsNullOrEmpty(value)) ? value : defaultValue;
        }
        private static void writeValue(string pstrValueToRead, string pstrValueToWrite)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                if (File.Exists(SettingsPath))
                {
                    using (var reader = new XmlTextReader(SettingsPath))
                    {
                        doc.Load(reader);
                    }
                }
                else
                {
                    var dir = Path.GetDirectoryName(SettingsPath);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    doc.AppendChild(doc.CreateElement("settings"));
                }

                XmlElement root = doc.DocumentElement;
                XmlNode oldNode = root.SelectSingleNode(@"/settings/" + pstrValueToRead);
                if (oldNode == null) // create if not exist
                {
                    oldNode = doc.SelectSingleNode("settings");
                    oldNode.AppendChild(doc.CreateElement(pstrValueToRead)).InnerText = pstrValueToWrite;
                    doc.Save(SettingsPath);
                    return;
                }
                oldNode.InnerText = pstrValueToWrite;
                doc.Save(SettingsPath);
            }
            catch
            {
            }
        }

        #endregion
    }
}
