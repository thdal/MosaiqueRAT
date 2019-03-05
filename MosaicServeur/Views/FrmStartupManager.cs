using Serveur.Controllers.Server;
using Serveur.Packets.ServerPackets;
using System;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmStartupManager : Form
    {
        private readonly ClientMosaic _client;

        public FrmStartupManager(ClientMosaic client)
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
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
