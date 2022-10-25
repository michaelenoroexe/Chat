
using Servers;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {            
            using (IServer server = ChatServerBuilder.GetServer())
            {
                server.StartServer();

                Console.ReadKey();
            }
        }
    }
}
