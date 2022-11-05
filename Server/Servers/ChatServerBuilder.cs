using Servers.Chat;
using Servers.Connection;
using Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Servers
{
    public static class ChatServerBuilder
    {
        private static ChatServer _instance;

        /// <summary>
        /// Get instance of chat server
        /// </summary>
        /// <returns></returns>
        public static IServer GetServer() 
        { 
            if (_instance != null) return _instance;
            return new ChatServer();      
        }

    }
}
