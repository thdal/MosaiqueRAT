using Serveur.Controllers.Server;
using System;
using System.IO;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmKeylogger : Form
    {
        private readonly ClientMosaic _client;
        private readonly string _path;

        public FrmKeylogger(ClientMosaic client)
        {
            _client = client;
            client.value.frmKl = this;
            _path = Path.Combine(client.value.downloadDirectory, "Logs\\");
            InitializeComponent();
        }

        private void FrmKeylogger_Load(object sender, System.EventArgs e)
        {
            if (_client != null)
            {
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                    return;
                }

                DirectoryInfo dicInfo = new DirectoryInfo(_path);
                FileInfo[] iFiles = dicInfo.GetFiles();

                foreach(FileInfo file in iFiles)
                {
                    lvLogs.Items.Add(new ListViewItem() { Text = file.Name });
                }
            }
        }

        private void btnGetLogs_Click(object sender, System.EventArgs e)
        {
            btnGetLogs.Enabled = false;
            lvLogs.Items.Clear();

            //new GetKeyLoggerLogs().Execute(_client);
        }

        private void lvLogs_ItemActivate(object sender, System.EventArgs e)
        {
            if(lvLogs.SelectedItems.Count > 0)
            {
                wbLogViewver.Navigate(Path.Combine(_path, lvLogs.SelectedItems[0].Text));
            }
        }

        private void FrmKeylogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_client.value != null)
            {
                _client.value.frmKl = null;
            }
        }

        public void AddLogToListview(string logName)
        {
            try
            {
                lvLogs.Invoke((MethodInvoker)delegate
                {
                    lvLogs.Items.Add(new ListViewItem { Text = logName });
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void SetGetLogsEnabled(bool enabled)
        {
            try
            {
                btnGetLogs.Invoke((MethodInvoker)delegate
                {
                    btnGetLogs.Enabled = enabled;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
