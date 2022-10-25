using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Connection
{
    /// <summary>
    /// Factory to create connection between two users
    /// </summary>
    internal abstract class ConnectionFactory
    {
        /// <summary>
        /// Create connection between two users 
        /// </summary>
        /// <returns>Connection between two users to store it</returns>
        public abstract IConnection SetConnection(IConnectUser firstUser, IConnectUser secondUser);
    }
}
