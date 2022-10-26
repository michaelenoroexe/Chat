using Servers.Connection;

namespace Servers.Chat
{
    /// <summary>
    /// Class to create special to chat connections
    /// </summary>
    internal class ChatConnectionFactory : IConnectionFactory<string>
    {
        public IConnection<string> SetConnection(IConnectUser<string> firstUser, IConnectUser<string> secondUser)
        {
            ChatConnect con = new ChatConnect(firstUser, secondUser);

            firstUser.SetConnection(con);
            secondUser.SetConnection(con);

            return con;
        }
        /// <summary>
        /// Class, that objects creates ChatConnectionFactory
        /// </summary>
        internal class ChatConnect : IConnection<string>
        {
            private IConnectUser<string> firstUser;
            private IConnectUser<string> secondUser;

            public ChatConnect(IConnectUser<string> firstUser, IConnectUser<string> secondUser)
            {
                this.firstUser = firstUser;
                this.secondUser = secondUser;
            }
            // Send message to oposite user of connection
            public void Send(string msg, IConnectUser<string> user)
            {
                if (user == firstUser)
                {
                    secondUser.Notify(msg);
                }
                else
                {
                    firstUser.Notify(msg);
                }
            }
        }
    }
}
