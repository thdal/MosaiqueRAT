using Client.Packets.ServerPackets;
using Client.Packets.ClientPackets;
using Microsoft.Win32;
using System;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;
using System.Xml;
using System.Management;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Client.Controllers
{
    public static class AuthenticationController
    {
        public static int ImageIndex { get; set; }
        public static GeoInformation GeoInfo { get; private set; }
        public static DateTime LastLocated { get; private set; }
        public static bool LocationCompleted { get; private set; }

        public static void HandleGetAuthentication(ClientMosaic client)
        {
            geoLocationInitialize();
            new GetAuthenticationResponse("01", getOperatingSystem(), getAccountType(),
                GeoInfo.Country, GeoInfo.CountryCode, "", 0, devicesHelper(), getName()).Execute(client);
        }

        public static string devicesHelper()
        {
            using (var sha = new SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(getCpuName()  + getBiosIdentifier() + getMainboardIdentifier());
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static string getBiosIdentifier()
        {
            try
            {
                string biosIdentifier = string.Empty;
                string query = "SELECT * FROM Win32_BIOS";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        biosIdentifier = mObject["Manufacturer"].ToString();
                        break;
                    }
                }

                return (!string.IsNullOrEmpty(biosIdentifier)) ? biosIdentifier : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        public static string getMainboardIdentifier()
        {
            try
            {
                string mainboardIdentifier = string.Empty;
                string query = "SELECT * FROM Win32_BaseBoard";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        mainboardIdentifier = mObject["Manufacturer"].ToString() + mObject["SerialNumber"].ToString();
                        break;
                    }
                }

                return (!string.IsNullOrEmpty(mainboardIdentifier)) ? mainboardIdentifier : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        public static string getCpuName()
        {
            try
            {
                string cpuName = string.Empty;
                string query = "SELECT * FROM Win32_Processor";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        cpuName += mObject["Name"].ToString() + "; ";
                    }
                }
                cpuName = removeEnd(cpuName);

                return (!string.IsNullOrEmpty(cpuName)) ? cpuName : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        public static string removeEnd(string input)
        {
            if (input.Length > 2)
                input = input.Remove(input.Length - 2);
            return input;
        }

        public static string getOperatingSystem()
        {
            string Name = "Unknown OS";

            try
            {
                Name = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "").ToString();
            }
            catch
            {
                using (
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem"))
                {
                    foreach (ManagementObject os in searcher.Get())
                    {
                        Name = os["Caption"].ToString();
                        break;
                    }
                }
            }

            Name = Regex.Replace(Name, "^.*(?=Windows)", "").TrimEnd().TrimStart(); // Remove everything before first match "Windows" and trim end & start
            bool Is64Bit = Environment.Is64BitOperatingSystem;
            string FullName = string.Format("{0} {1} Bit", Name, Is64Bit ? 64 : 32);
            return FullName;
        }

        public static string getAccountType()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                if (identity != null)
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);

                    if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                        return "Admin";
                    if (principal.IsInRole(WindowsBuiltInRole.User))
                        return "User";
                    if (principal.IsInRole(WindowsBuiltInRole.Guest))
                        return "Guest";
                }
            }
            return "Unknown";
        }

        public static string getName()
        {
            string Name = "";

            try
            {
                Name = Environment.UserName;
            }
            catch
            {
                Name = "?";
            }
            try
            {
                Name += '@' + Environment.MachineName;
            }
            catch
            {
                Name += '@' + '?';
            }

            return Name;
        }

        public static void geoLocationInitialize()
        {
            LastLocated = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan lastLocateTry = new TimeSpan(DateTime.UtcNow.Ticks - LastLocated.Ticks);

            // last location was 30 minutes ago or last location has not completed
            if (lastLocateTry.TotalMinutes > 30 || !LocationCompleted)
            {
                TryLocate();

                if (GeoInfo.CountryCode == "-" || GeoInfo.Country == "Unknown")
                {
                    ImageIndex = 247; // question icon
                    return;
                }
                /*
                for (int i = 0; i < ImageList.Length; i++)
                {
                    if (ImageList[i].Contains(GeoInfo.country_code.ToLower()))
                    {
                        ImageIndex = i;
                        break;
                    }
                }*/
            }
        }

        private static void TryLocate()
        {
            LocationCompleted = false;

            try
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GeoInformation));

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://ip-api.com/json");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
                request.Proxy = null;
                request.Timeout = 10000;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseString = reader.ReadToEnd();

                            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(responseString)))
                            {
                                GeoInfo = (GeoInformation)jsonSerializer.ReadObject(ms);
                            }
                        }
                    }
                }

                LastLocated = DateTime.UtcNow;
                LocationCompleted = true;
            }
            catch
            {
                TryLocateFallback();
            }
        }

        private static void TryLocateFallback()
        {
            GeoInfo = new GeoInformation();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://freegeoip.net/");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
                request.Proxy = null;
                request.Timeout = 10000;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseString = reader.ReadToEnd();

                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(responseString);

                            string xmlIp = doc.SelectSingleNode("Response//IP").InnerXml;
                            string xmlCountry = doc.SelectSingleNode("Response//CountryName").InnerXml;
                            string xmlCountryCode = doc.SelectSingleNode("Response//CountryCode").InnerXml;
                            string xmlRegion = doc.SelectSingleNode("Response//RegionName").InnerXml;
                            string xmlCity = doc.SelectSingleNode("Response//City").InnerXml;
                            string timeZone = doc.SelectSingleNode("Response//TimeZone").InnerXml;

                            GeoInfo.Ip = (!string.IsNullOrEmpty(xmlIp))
                                ? xmlIp
                                : "-";
                            GeoInfo.Country = (!string.IsNullOrEmpty(xmlCountry))
                                ? xmlCountry
                                : "Unknown";
                            GeoInfo.CountryCode = (!string.IsNullOrEmpty(xmlCountryCode))
                                ? xmlCountryCode
                                : "-";
                            GeoInfo.Region = (!string.IsNullOrEmpty(xmlRegion))
                                ? xmlRegion
                                : "Unknown";
                            GeoInfo.City = (!string.IsNullOrEmpty(xmlCity))
                                ? xmlCity
                                : "Unknown";
                            GeoInfo.Timezone = (!string.IsNullOrEmpty(timeZone))
                                ? timeZone
                                : "Unknown";

                            GeoInfo.Isp = "Unknown"; // freegeoip does not support ISP detection
                        }
                    }
                }

                LastLocated = DateTime.UtcNow;
                LocationCompleted = true;
            }
            catch
            {
                GeoInfo.Country = "Unknown";
                GeoInfo.CountryCode = "-";
                GeoInfo.Region = "Unknown";
                GeoInfo.City = "Unknown";
                GeoInfo.Timezone = "Unknown";
                GeoInfo.Isp = "Unknown";
                LocationCompleted = false;
            }

            if (string.IsNullOrEmpty(GeoInfo.Ip))
                TryGetWanIp();
        }

        private static void TryGetWanIp()
        {
            string wanIp = "-";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.ipify.org/");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
                request.Proxy = null;
                request.Timeout = 5000;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            wanIp = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            GeoInfo.Ip = wanIp;
        }
    }

    [DataContract]
    public class GeoInformation
    {
        [DataMember(Name = "as")]
        public string As { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "countryCode")]
        public string CountryCode { get; set; }
        [DataMember(Name = "isp")]
        public string Isp { get; set; }
        [DataMember(Name = "lat")]
        public double Lat { get; set; }
        [DataMember(Name = "lon")]
        public double Lon { get; set; }
        [DataMember(Name = "org")]
        public string Org { get; set; }
        [DataMember(Name = "query")]
        public string Ip { get; set; }
        [DataMember(Name = "region")]
        public string Region { get; set; }
        [DataMember(Name = "regionName")]
        public string RegionName { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }
        [DataMember(Name = "zip")]
        public string Zip { get; set; }
    }
}
