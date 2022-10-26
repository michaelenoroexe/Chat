using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Connect
{
    internal class ChatConnect : IConnect<string>, IDisposable
    {
        private StreamReader _serverMessagesReader;
        private StreamWriter _messagesWriter;
        private Task _listener;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _disposedValue;

        /// <summary>
        /// Listen for incomming messages.
        /// </summary>
        private async Task Listener(CancellationToken? token = null)
        {
            string? line;

            while (token is null || !token.Value.IsCancellationRequested)
            {
                line = await _serverMessagesReader.ReadLineAsync();
                if (line == null) break;
                
                MessageReceived?.Invoke(this, new MessageEventArgs<string>(line));
            }
        }

        /// <summary>
        /// Occurs when user receive message.
        /// </summary>
        public event EventHandler<MessageEventArgs<string>>? MessageReceived;
        /// <summary>
        /// Connect to chat server.
        /// </summary>
        public ChatConnect(string ip, int port)
        {
            var client = new TcpClient(ip, port);
            var clientStream = client.GetStream();

            _serverMessagesReader = new StreamReader(clientStream);
            _messagesWriter = new StreamWriter(clientStream);

            _cancellationTokenSource = new CancellationTokenSource();
            _listener = Listener(_cancellationTokenSource.Token);
        }
        /// <summary>
        /// Send string message to remoute host.
        /// </summary>
        /// <param name="message">Text of sending message.</param>
        public void Send(string message)
        {
            _messagesWriter.WriteLine(message);
            // Commit message to stream.
            _messagesWriter.Flush();
        }
        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _cancellationTokenSource.Cancel();
                }

                _listener = null!;
                _disposedValue = true;
            }
        }
        /// <summary>
        /// Dispose connection resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
