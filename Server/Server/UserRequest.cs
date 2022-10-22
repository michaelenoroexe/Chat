using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal sealed class UserRequest : IDisposable
    {
        private readonly TcpClient _request;
        private readonly Stream _reqBody;
        private readonly int _userBodyLength;
        private bool disposedValue;
        // Cached connection key
        private int _connKey = -1;
        /// <summary>
        /// Return IP of client that make this request
        /// </summary>
        public IPAddress IP
        {
            get
            {
                return ((IPEndPoint)_request.Client.RemoteEndPoint!).Address;
            }
        }
        /// <summary>
        /// Return Connection key writed by client
        /// </summary>
        public int ConnectionKey
        {
            get
            {
                if (_connKey != -1) return _connKey;
                // if request body is null user dont have connection key
                if (_reqBody == null) return -1;

                var key = new byte[4];

                _reqBody.Read(key, 0, 4);

                _connKey = BitConverter.ToInt32(key);

                return _connKey;
            }
        }
        /// <summary>
        /// Return client request body string
        /// </summary>
        public string SendedTextString
        {
            get
            {
                // if request body is null user dont send any text
                if (_reqBody == null) return "";

                var text = new byte[_userBodyLength];

                _reqBody.Read(text, 0, text.Length-1);

                return Encoding.Unicode.GetString(text, 4, text.Length - 4);
            }
        }
        /// <summary>
        /// Return client request body byte arr
        /// </summary>
        public byte[] SendedTextBytes
        {
            get
            {
                // if request body is null user dont send any text
                if (_reqBody == null) return null;

                var text = new byte[_userBodyLength];

                _reqBody.Read(text, 0, text.Length - 1);

                return text.Skip(4).ToArray();
            }
        }
        // Private constructor for tests only
        private UserRequest() { }
        public UserRequest(TcpClient req)
        {
            _request = req;
            _reqBody = _request.GetStream();
            _userBodyLength = (int)_reqBody.Length;
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _reqBody.Dispose();
                    _request.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
