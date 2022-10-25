using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers
{
    public interface IServer
    {
        public void StartServer();

        public void StopServer();
    }
}
