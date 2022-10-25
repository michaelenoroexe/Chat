using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers.Connection
{
    internal interface IConnectUser
    {
        public void SetConnection(IConnection con);

        public void Send(string message);
    }
}
