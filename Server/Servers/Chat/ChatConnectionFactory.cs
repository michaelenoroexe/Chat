using Servers.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Chat
{
    /// <summary>
    /// Class to create special to chat connections
    /// </summary>
    internal class ChatConnectionFactory : ConnectionFactory
    {
        public override IConnection SetConnection(IConnectUser firstUser, IConnectUser secondUser)
        {
            ChatConnect con = new ChatConnect(firstUser, secondUser);

            firstUser.SetConnection(con);
            secondUser.SetConnection(con);

            return con;
        }
        /// <summary>
        /// Class, that objects creates ChatConnectionFactory
        /// </summary>
        internal class ChatConnect : IConnection
        {
            private IConnectUser firstUser;
            private IConnectUser secondUser;

            public ChatConnect(IConnectUser firstUser, IConnectUser secondUser)
            {
                this.firstUser = firstUser;
                this.secondUser = secondUser;
            }
            // Send message to oposite user of connection
            public void Send(string msg, IConnectUser user)
            {
                if (user == firstUser)
                {
                    secondUser.Send(msg);
                }
                else
                {
                    firstUser.Send(msg);
                }
            }
        }
    }
}
