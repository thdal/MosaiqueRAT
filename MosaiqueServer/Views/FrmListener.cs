using Serveur.Controllers;
using Serveur.Models;
using System;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmListener : Form
    {
        private FrmListenerController _frmListenerController;

        public FrmListener(FrmListenerController frmListenerController)
        {
            _frmListenerController = frmListenerController;
            InitializeComponent();
        }

        private void FrmListener_Load(object sender, EventArgs e)
        {
            txtPort.Value = ListenerState.listenPort;
                
            if (ListenerState.startListen == true)
            {
                btnListen.Text = "Stop listening";
            }

            chkStartupConnections.Checked = ListenerState.autoListen;
            chkPopupNotification.Checked = ListenerState.showPopup;
            chkIPv6.Checked = ListenerState.IPv6Support;
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if(btnListen.Text =="Start listening")
            {
                ListenerState.listenPort = Convert.ToInt32(txtPort.Text);
                ListenerState.startListen = true;
                _frmListenerController.listen(int.Parse(txtPort.Text), chkIPv6.Checked);
                btnListen.Text = "Stop listening";
            }
            else
            {
                ListenerState.startListen = false;
                _frmListenerController.stopListening();
                btnListen.Text = "Start listening";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ListenerState.autoListen = chkStartupConnections.Checked;
            ListenerState.showPopup = chkPopupNotification.Checked;
            ListenerState.IPv6Support = chkIPv6.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
