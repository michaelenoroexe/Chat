
using Servers;

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
