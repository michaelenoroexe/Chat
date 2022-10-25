using Servers.Connection;
using Servers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servers
{
    internal sealed class ChatServer : IServer
    {
        private static ChatServer instance;
        private TcpClient? waiting;
        private TcpListener listener;
        private List<IConnection> storage;

        private ChatServer()
        {
            listener = new TcpListener(IPAddress.Any, 5567);
            storage = new List<IConnection>();
            waiting = null;
        }
        /// <summary>
        /// Accept all receiving requests to connect
        /// </summary>
        /// <returns></returns>
        private async Task AcceptConnections()
        {
            TcpClient client;
            ChatConnectionFactory connFactory = new ChatConnectionFactory();

            while (true)
            {
                client = listener.AcceptTcpClient();

                if (waiting is not null)
                {
                    storage.Add(connFactory.SetConnection(waiting, client));
                    waiting = null;

                    continue;
                    
                }
                // If no one dont wait to chat make user wait
                waiting = client;
            }
        }

        /// <summary>
        /// Get instance of server
        /// </summary>
        /// <returns></returns>
        public static IServer GetServer() 
        { 
            if (instance != null) return instance;
            return new ChatServer();      
        }

        public void StartServer()
        {
            throw new NotImplementedException();
        }

        public void StopServer()
        {
            throw new NotImplementedException();
        }

    }
}
