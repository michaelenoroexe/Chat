using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Connection
{
    internal interface IConnectUser<T>
    {
        public void SetConnection(IConnection<T> con);

        public void Notify(T message);
    }
}
