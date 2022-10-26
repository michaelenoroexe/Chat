using Servers.Connection;
using System.Net.Sockets;

namespace Servers.Chat
{
    /// <summary>
    /// User of the chat server, can exchange data with linked user
    /// </summary>
    internal sealed class ChatUser : ConnectUser<string>, IDisposable
    {
        private TcpClient _tcpClient;
        private StreamWriter _messageWriter;
        private StreamReader _messageReader;
        private Task _listen;
        private bool _disposedValue;

        /// <summary>
        /// Listen on client inputed messages and send it to anouther client
        /// </summary>
        private void ListenRequests()
        {
            string? line;
            while (true)
            {
                line = _messageReader.ReadLine();
                if (line == null) break;
                Send(line);
            }
        }

        public ChatUser(TcpClient client)
        {
            _tcpClient = client;

            var userStream = _tcpClient.GetStream();

            _messageWriter = new StreamWriter(userStream);
            _messageReader = new StreamReader(userStream);

            _listen = Task.Run(ListenRequests);
        }
        /// <summary>
        /// Send message to user
        /// </summary>
        /// <param name="message"></param>
        public override void Notify(string message)
        {
            _messageWriter.WriteLine(message);
            // Commit message to user stream.
            _messageWriter.Flush();
        }
        #region Dispose
        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _listen.Dispose();
                    _messageReader.Dispose();
                    _messageWriter.Dispose();
                    _tcpClient.Dispose();
                }

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
