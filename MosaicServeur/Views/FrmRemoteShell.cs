using Serveur.Controllers;
using Serveur.Controllers.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmRemoteShell : Form
    {
        //Instance de classe
        public FrmRemoteShellController frmremoteShellController;
        private readonly ClientMosaic _clientMosaic;

        public FrmRemoteShell(ClientMosaic client)
        {
            _clientMosaic = client;
            _clientMosaic.value.frmRms = this;
            frmremoteShellController = new FrmRemoteShellController(client);
            InitializeComponent();
        }

        private void FrmRemoteShell_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            if(_clientMosaic != null)
            {

            }
        }
    }
}
