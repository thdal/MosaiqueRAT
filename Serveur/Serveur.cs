using Serveur.Controllers.Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Serveur
{
    public partial class Serveur : Form
    {

        private static readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> _clientSockets = new List<Socket>();
        public static List<ClientMosaic> _clientControllers;
        private const int BUFFER_SIZE = 2048;
        private const int port = 4444;
        private static readonly byte[] _buffer = new byte[BUFFER_SIZE];

        public Serveur()
        {
            InitializeComponent();
            Listen();
        }

        private void Listen()
        {
            try
            {
                _clientControllers = new List<ClientMosaic>();
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                _serverSocket.Listen(0);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = _serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            _clientSockets.Add(socket);
            ClientMosaic newClient = new ClientMosaic(socket);
            _clientControllers.Add(newClient);
            _serverSocket.BeginAccept(AcceptCallback, null);
        }
       

        private void AppendToTextBox(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate
            {
                textBox1.Text += text;
            });

            this.Invoke(invoker);
        }
    }
}
