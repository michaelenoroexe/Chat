using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Connection
{
    internal interface IConnection<T>
    {
        public void Send(T msg, IConnectUser<T> user);
    }
}
