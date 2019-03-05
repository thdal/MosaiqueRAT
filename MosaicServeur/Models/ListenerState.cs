using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Serveur.Models
{
   public static class ListenerState
    {
        private static readonly string settingsPath = Path.Combine(Application.StartupPath, "settings.xml");


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
        public static bool IPv6Support
        {
            get
            {
                return bool.Parse(readValueSafe("IPv6Support", "false"));
            }
            set
            {
                writeValue("IPv6Support", value.ToString());
            }
        }
        private static string readValue(string pstrValueToRead)
        {
            try
            {
                XPathDocument doc = new XPathDocument(settingsPath);
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

                if (File.Exists(settingsPath))
                {
                    using (var reader = new XmlTextReader(settingsPath))
                    {
                        doc.Load(reader);
                    }
                }
                else
                {
                    var dir = Path.GetDirectoryName(settingsPath);
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
                    doc.Save(settingsPath);
                    return;
                }
                oldNode.InnerText = pstrValueToWrite;
                doc.Save(settingsPath);
            }
            catch
            {
            }
        }
    }
}
