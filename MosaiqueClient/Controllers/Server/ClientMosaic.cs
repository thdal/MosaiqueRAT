using Client.Controllers.Tools;
using Client.Packets;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using ZeroFormatter;

namespace Client.Controllers
{
    public class ClientMosaic
    {
        //SOCKET
        private Socket _clientSocket;
        private int PORT;
        private string HOST;
        //BUFFER
        private const int BUFFER_SIZE = 1084 * 16;
        public int HEADER_SIZE { get { return 4; } } // 4B   
        //RECEIVE
        private readonly Queue<byte[]> _readBuffers = new Queue<byte[]>();
        private readonly object _readingPacketsLock = new object();
        private bool _readingPackets;
        private bool _appendHeader;
        private byte[] _readBuffer;
        private byte[] _payloadBuffer;
        private byte[] _tempHeader;
        private int _tempHeaderOffset;
        private int _payloadLen;
        private int _readableDataLen;
        private int _readOffset;
        private int _writeOffset;
        public int MAX_PACKET_SIZE { get { return (1024 * 1024) * 5; } } // 5MB    
        private ReceiveType _receiveState = ReceiveType.Header;
        public enum ReceiveType
        {
            Header,
            Payload
        }
        //SEND
        private readonly Queue<byte[]> _sendBuffers = new Queue<byte[]>();
        private bool _sendingPackets;
        private readonly object _sendingPacketsLock = new object();
        //STATE
        public static bool EXITING { get; private set; }
        public static bool testexit = true;
        public bool authenticated { get; private set; }
        public bool connected { get; private set; }

        //CONSTRUCTEUR
        public ClientMosaic(string host, int port)
        {
            HOST = host;
            PORT = port;
        }

        //CONNECT TO SERVER
        public void connect()
        {
            while (!EXITING)
            {
                if (!connected)
                {
                    try
                    {
                        disconnect();

                        _readBuffer = new byte[BUFFER_SIZE];

                        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        _clientSocket.Connect(HOST, PORT);

                        _tempHeader = new byte[HEADER_SIZE]; // ?????


                        if (_clientSocket.Connected)
                        {
                            _clientSocket.BeginReceive(_readBuffer, 0, _readBuffer.Length, SocketFlags.None, receiveCallBack, null);
                            connected = true;
                        }

                        //Application.DoEvents();

                    }
                    catch (Exception)
                    {
                        disconnect();
                    }
                }

                while (connected) // hold client open
                {
                    Application.DoEvents();
                    Thread.Sleep(2500);
                }

                if (EXITING)
                {
                    disconnect();
                    return;
                }
            }
        }

        #region RECEIVE
        private void receiveCallBack(IAsyncResult AR)
        {
            int received;

            try
            {
                received = _clientSocket.EndReceive(AR);

                if (received <= 0)
                    throw new Exception("no bytes transferred");
            }
            catch (NullReferenceException)
            {
                return;
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (Exception)
            {
                disconnect();
                return;
            }

            byte[] buffer = new byte[received];

            try
            {
                Array.Copy(_readBuffer, buffer, buffer.Length);               
            }
            catch
            {
                disconnect();
                return;
            }

            lock (_readBuffers)
            {
                _readBuffers.Enqueue(buffer);
            }

            lock (_readingPacketsLock)
            {
                if (!_readingPackets)
                {
                    _readingPackets = true;
                    ThreadPool.QueueUserWorkItem(asyncReceive);
                }
            }

            try
            {
                _clientSocket.BeginReceive(_readBuffer, 0, BUFFER_SIZE, SocketFlags.None, receiveCallBack, null);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception)
            {
                disconnect();
            }
        }

        private void asyncReceive(object state)
        {
            while (true)
            {
                byte[] readBuffer;

                lock (_readBuffers)
                {
                    if(_readBuffers.Count == 0)
                    {
                        lock (_readingPacketsLock)
                        {
                            _readingPackets = false;
                        }
                        return;
                    }

                    readBuffer = _readBuffers.Dequeue();
                }

                _readableDataLen += readBuffer.Length;
                bool process = true;

                while (process)
                {
                    switch (_receiveState)
                    {
                        case ReceiveType.Header: // PayloadLength
                            {
                                if(_readableDataLen >= HEADER_SIZE)
                                {
                                    int headerLength = (_appendHeader) ? HEADER_SIZE - _tempHeaderOffset : HEADER_SIZE;

                                    try
                                    {
                                        if (_appendHeader)
                                        {
                                            try
                                            {
                                                Array.Copy(readBuffer, _readOffset, _tempHeader, _tempHeaderOffset,
                                                    headerLength);
                                            }
                                            catch (Exception)
                                            {
                                                process = false;
                                                disconnect();
                                                break;
                                            }
                                            _payloadLen = BitConverter.ToInt32(_tempHeader, 0);
                                            _tempHeaderOffset = 0;
                                            _appendHeader = false;                                            
                                        }
                                        else
                                        {
                                            _payloadLen = BitConverter.ToInt32(readBuffer, _readOffset);
                                        }

                                        if (_payloadLen <= 0 || _payloadLen > MAX_PACKET_SIZE)
                                            throw new Exception("invalid header");
                                    }
                                    catch (Exception)
                                    {
                                        process = false;
                                        disconnect();
                                        break;
                                    }

                                    _readableDataLen -= headerLength;
                                    _readOffset += headerLength;
                                    _receiveState = ReceiveType.Payload;

                                }
                                else//  _readableDataLen < HEADER_SIZE
                                {
                                    try
                                    {
                                        Array.Copy(readBuffer, _readOffset, _tempHeader, _tempHeaderOffset, _readableDataLen);
                                    }
                                    catch (Exception)
                                    {
                                        process = false;
                                        disconnect();
                                        break;
                                    }
                                    _tempHeaderOffset += _readableDataLen;
                                    _appendHeader = true;
                                    process = false;
                                }
                                break;
                            }
                        case ReceiveType.Payload: //Deserialize Payload
                            {
                                if (_payloadBuffer == null || _payloadBuffer.Length != _payloadLen)
                                    _payloadBuffer = new byte[_payloadLen];

                                int length = (_writeOffset + _readableDataLen >= _payloadLen) ? _payloadLen - _writeOffset : _readableDataLen;


                                try
                                {
                                    Array.Copy(readBuffer, _readOffset, _payloadBuffer, _writeOffset, length);
                                }
                                catch (Exception)
                                {
                                    process = false;
                                    disconnect();
                                    break;
                                }

                                _writeOffset += length;
                                _readOffset += length;
                                _readableDataLen -= length;

                                if(_writeOffset == _payloadLen)
                                {
                                    bool isError = _payloadBuffer.Length == 0;

                                    if (isError)
                                    {
                                        process = false;
                                        disconnect();
                                        break;
                                    }

                                    try
                                    {
                                        IPackets packet = ZeroFormatterSerializer.Deserialize<IPackets>(_payloadBuffer);
                                        packetReader(packet);
                                    }
                                    catch (Exception)
                                    {
                                        process = false;
                                        disconnect();
                                        break;
                                    }

                                    _receiveState = ReceiveType.Header;
                                    _payloadBuffer = null;
                                    _payloadLen = 0;
                                    _writeOffset = 0;
                                }

                                if(_readableDataLen == 0)
                                {
                                    process = false;
                                }
                                break;
                            }
                    }
                }

                if(_receiveState == ReceiveType.Header)
                {
                    _writeOffset = 0;
                }
                _readOffset = 0;
                _readableDataLen = 0;
            }
        }
        #endregion

        #region SEND
        public void send<T>(T packet) where T : IPackets
        {
            connected = true;
            byte[] payload;
            if (!connected || packet == null) return;
            lock (_sendBuffers)
            {
                try
                {
                    payload = ZeroFormatterSerializer.Serialize<IPackets>(packet);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                    disconnect();
                    return;
                }

                _sendBuffers.Enqueue(payload);


                lock (_sendingPacketsLock)
                {
                    if (_sendingPackets) return;

                    _sendingPackets = true;
                }
                ThreadPool.QueueUserWorkItem(Send);
            }
        }

        public void sendBlocking<T>(T packet) where T : IPackets
        {
            send(packet);
            while (_sendingPackets)
            {
                Thread.Sleep(10);
            }
        }

        private void Send(object state)
        {
            while (true)
            {
                if (!connected)
                {
                    SendCleanup(true);
                    return;
                }

                byte[] payload;
                lock (_sendBuffers)
                {
                    if (_sendBuffers.Count == 0)
                    {
                        SendCleanup();
                        return;
                    }

                    payload = _sendBuffers.Dequeue();
                }

                try
                {
                    var packet = packetBuilder(payload);
                    //_parentServer.BytesSent += packet.Length;
                    _clientSocket.Send(packet);
                    _sendingPackets = false;
                }
                catch (Exception)
                {
                    disconnect();
                    SendCleanup(true);
                    return;
                }
            }
        }
        #endregion

        //MANAGE PACKET 
        private byte[] packetBuilder(byte[] payload)
        {
            byte[] packet_ = new byte[payload.Length + HEADER_SIZE];
            Array.Copy(BitConverter.GetBytes(payload.Length), packet_, HEADER_SIZE);
            Array.Copy(payload, 0, packet_, HEADER_SIZE, payload.Length);
            return packet_;
        }

        private void SendCleanup(bool clear = false)
        {
            lock (_sendingPacketsLock)
            {
                _sendingPackets = false;
            }

            if (!clear) return;

            lock (_sendBuffers)
            {
                _sendBuffers.Clear();
            }
        }

        private void packetReader(IPackets packet)
        {
            var type = packet.Type;

            if (!authenticated)
            {
                if (type == TypePackets.GetAuthentication)
                {
                    AuthenticationController.HandleGetAuthentication(this);
                }
                else if (type == TypePackets.SetAuthenticationSuccess)
                {
                    authenticated = true;
                }
                return;
            }

            PacketHandler.packetChecker(this, packet);
        }

        //EVENT
        public void disconnect()
        {
            if (_clientSocket != null)
            {
                _clientSocket.Close();
                _clientSocket = null;
                _readOffset = 0;
                _writeOffset = 0;
                _tempHeaderOffset = 0;
                _payloadLen = 0;
                _payloadBuffer = null;
                _receiveState = ReceiveType.Header;
            } 

            connected = false;
            authenticated = false;
        }

        public void Exit()
        {
            EXITING = true;
            disconnect();
        }
    }
}
