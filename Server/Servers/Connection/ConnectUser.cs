using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Connection
{
    internal class ConnectUser : IConnectUser
    {
        protected IConnection? connect = null;
        /// <summary>
        /// Set established connection to property
        /// </summary>
        /// <param name="con"></param>
        public virtual void SetConnection(IConnection con)
        {
            connect = con;
        }
        /// <summary>
        /// Send message to aniuther user with established connection
        /// </summary>
        /// <param name="message">Message that will be sended to opposite user of connection</param>
        /// <exception cref="InvalidOperationException">If the connection is not established</exception>
        public virtual void Send(string message)
        {
            if (connect == null) throw new InvalidOperationException("Connection is not assign");
            connect.Send(message, this);
        }
    }
}
