using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servers
{
    public interface IServer : IDisposable
    {
        /// <summary>
        /// Start of server work
        /// </summary>
        public void StartServer();
        /// <summary>
        /// Stop server
        /// </summary>
        public void StopServer();
    }
}
