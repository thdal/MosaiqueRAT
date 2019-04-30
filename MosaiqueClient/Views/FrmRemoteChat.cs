using Client.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Views
{
    public partial class FrmRemoteChat : Form
    {
        private ClientMosaique _client;

        public FrmRemoteChat(ClientMosaique client)
        {
            _client = client;
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            updateRTxtChat("[" + DateTime.Now.ToShortTimeString() + "]", Color.Blue);
            updateRTxtChat(' ' + "You : ", Color.Black);
            updateRTxtChat(txtWrite.Text + Environment.NewLine, Color.Black);
            new Packets.ServerPackets.MsgToRemoteChat(txtWrite.Text).Execute(_client);
            txtWrite.Text = string.Empty;
        }

        private void FrmRemoteChat_Load(object sender, EventArgs e)
        {
            txtWrite.KeyDown += new KeyEventHandler(txtWrite_KeyDown);
        }

        private void FrmRemoteChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_client.frmRChat != null)
            {
                _client.frmRChat = null;
            }
        }

        public void updateRTxtChat(string text, Color color)
        {
            try
            {
                rtxtChat.Invoke((MethodInvoker)delegate
                {
                    rtxtChat.SelectionStart = rtxtChat.TextLength;
                    rtxtChat.SelectionLength = 0;

                    rtxtChat.SelectionColor = color;
                    rtxtChat.AppendText(text);
                    rtxtChat.SelectionColor = rtxtChat.ForeColor;
                });
            }
            catch (InvalidOperationException)
            {

            }
        }

        private void txtWrite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtWrite.Text.Trim()))
            {
                btnSend_Click(this, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtWrite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)2)
            {
                txtWrite.Focus();
                txtWrite.ScrollToCaret();
            }
        }
    }
}
