using Servers.Connection;
using System.Net.Sockets;

namespace Servers.Chat
{
    /// <summary>
    /// User of the chat server, can exchange data with linked user.
    /// </summary>
    internal sealed class ChatUser : ConnectUser<string>, IDisposable
    {
        private StreamWriter _messageWriter;
        private StreamReader _messageReader;
        private Task _listen;
        private bool _disposedValue;

        /// <summary>
        /// Listen on client inputed messages and send it to anouther client.
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

        internal ChatUser(TcpClient client)
        {
            var userStream = client.GetStream();

            _messageWriter = new StreamWriter(userStream);
            _messageReader = new StreamReader(userStream);

            _listen = Task.Run(ListenRequests);
        }
        /// <summary>
        /// Send message to user.
        /// </summary>
        /// <param name="message">Message content.</param>
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
                }

                _disposedValue = true;
            }
        }
        /// <summary>
        /// Dispose user resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
