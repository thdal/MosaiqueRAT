using System.Diagnostics;

namespace Client.Controllers
{
    public static class TaskManagerController
    {
        public static void getProcesses(Packets.ServerPackets.GetProcesses packet, ClientMosaique client)
        {
            Process[] AllProcesses = Process.GetProcesses();
            string[] pNames = new string[AllProcesses.Length];
            int[] pIds = new int[AllProcesses.Length];
            string[] pTitles = new string[AllProcesses.Length];

            int i = 0;

            foreach (Process p in AllProcesses)
            {
                pNames[i] = p.ProcessName + ".exe";
                pIds[i] = p.Id;
                pTitles[i] = p.MainWindowTitle;
                i++;
            }

            new Packets.ClientPackets.GetProcessesResponse(pNames, pIds, pTitles).Execute(client);
        }
    }
}

