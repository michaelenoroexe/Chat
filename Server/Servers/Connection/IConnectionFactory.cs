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
    internal interface IConnectionFactory<T>
    {
        /// <summary>
        /// Create connection between two users 
        /// </summary>
        /// <returns>Connection between two users to store it</returns>
        public IConnection<T> SetConnection(IConnectUser<T> firstUser, IConnectUser<T> secondUser);
    }
}
