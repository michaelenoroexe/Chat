﻿using System;
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
        /// Return client request body string
        /// </summary>
        public string SendedTextString
        {
            get
            {
                var len = 0;
                // if request body is null user dont send any text
                if (_reqBody == null) return "";

                var text = new byte[1000];

                _reqBody.Read(text, 0, text.Length-1);

                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == 0 && text[i - 1] == 0) { len = i - 2; break; }
                }

                return Encoding.Unicode.GetString(text, 0, len);
            }
        }
        /// <summary>
        /// Return client request body byte arr
        /// </summary>
        public byte[] SendedTextBytes
        {
            get
            {
                var len = 0;
                // if request body is null user dont send any text
                if (_reqBody == null) return null;

                var text = new byte[1000];

                _reqBody.Read(text, 0, text.Length - 1);

                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == 0 && text[i - 1] == 0) { len = i - 2; break; }
                }

                return text.Take(len).ToArray();
            }
        }
        public void Send(byte[] wr)
        {
            _reqBody.Flush();
            _reqBody.Write(wr, 0, wr.Length);
        }
        // Private constructor for tests only
        private UserRequest() { }
        public UserRequest(TcpClient req)
        {
            _request = req;
            _reqBody = _request.GetStream();
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

        public static explicit operator TcpClient(UserRequest re)
        {
            return re._request;
        }
        public static explicit operator Stream(UserRequest re)
        {
            return re._reqBody;
        }

        public override int GetHashCode()
        {
            return this.IP.GetHashCode();
        }
        public bool Equals(UserRequest req)
        {
            return req.GetHashCode() == this.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(UserRequest)) return false;
            return this.Equals((UserRequest)obj);
        }
    }
}