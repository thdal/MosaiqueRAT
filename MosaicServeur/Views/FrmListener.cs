using Serveur.Controllers;
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
            txtPort.Value = FrmListenerController.listenPort;
            if (FrmListenerController.startListen == true)
            {
                btnListen.Text = "Stop listening";
            }
            //checkBox1.Checked = FrmListenerController.autoListen;
            //checkBox2.Checked = FrmListenerController.showPopup;
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if(btnListen.Text =="Start listening")
            {
                FrmListenerController.listenPort = Convert.ToInt32(txtPort.Text);
                FrmListenerController.startListen = true;
                _frmListenerController.listen(int.Parse(txtPort.Text));
                btnListen.Text = "Stop listening";
            }
            else
            {
                _frmListenerController.stopListening();
                FrmListenerController.startListen = false;
                btnListen.Text = "Start listening";
            }
        }
    }
}
