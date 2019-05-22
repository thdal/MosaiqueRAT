using MosaiqueServeur.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MosaiqueServeur.Views
{
    public partial class FrmHideShowFunctions: Form
    {

        public testclock t = new testclock();
        public FrmHideShowFunctions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //desktop
        {
            test.hidebutton_Click();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            test.showbutton_Click();
        }

        private void button4_Click(object sender, EventArgs e) // taskbar
        {

            test.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            test.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            t.button1_Click();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            t.button2_Click();
        }
    }
}
