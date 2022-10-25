using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Connection
{
    internal interface IConnection
    {
        public void Send(string msg, IConnectUser user);
    }
}
