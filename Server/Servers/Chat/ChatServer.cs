using Servers.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Chat
{
    internal sealed class ChatServer : IServer
    {
        private TcpClient? _waiting;
        private TcpListener _listener;
        private List<IConnection<string>> _storage;
        private Task _acceptConnects;
        private CancellationTokenSource _tokenSource;
        private bool _disposedValue;

        /// <summary>
        /// Accept all receiving requests to connect
        /// </summary>
        /// <returns></returns>
        private void AcceptConnections()
        {
            TcpClient client;
            ChatConnectionFactory connFactory = new ChatConnectionFactory();
            _listener.Start();
            while (true)
            {
                client = _listener.AcceptTcpClient();

                if (_waiting is not null)
                {
                    _storage.Add(connFactory.SetConnection(new ChatUser(_waiting), new ChatUser(client)));
                    _waiting = null;

                    continue;
                }
                // If no one dont wait to chat, make user wait
                _waiting = client;
            }
        }
        internal ChatServer()
        {
            _listener = new TcpListener(IPAddress.Any, 5567);
            _storage = new List<IConnection<string>>();
            _waiting = null;
            _tokenSource = new CancellationTokenSource();
            _acceptConnects = new Task(AcceptConnections);
        }

        /// <summary>
        /// Start receive client connections
        /// </summary>
        public void StartServer()
        {
            _acceptConnects.Start();
        }
        /// <summary>
        /// Dispose server resources
        /// </summary>
        public void StopServer()
        {
            Dispose();
        }
        #region Dispose
        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _tokenSource.Cancel();
                    _tokenSource.Dispose();
                    _acceptConnects.Dispose();
                    _listener.Stop();
                }

                _storage = null!;
                _waiting = null;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
