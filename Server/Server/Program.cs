
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        private static Queue<IPAddress> _reqQueue = new Queue<IPAddress>();

        private static Dictionary<int, Connection> _connStrings = new Dictionary<int, Connection>();

        static void Main(string[] args)
        {
            // Preparing sockets to listen 
            var listener = new TcpListener(IPAddress.Loopback, 5567);
            var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var keyBuff = new byte[4];
            
            //Listening begins
            listener.Start();
            while (true)
            {
                Console.WriteLine("Ожидание подключений... ");

                // Receiving connection
                TcpClient c = listener.AcceptTcpClient();
                UserRequest client = new UserRequest(c);

                if (_connStrings.TryGetValue(client.ConnectionKey, out Connection? connect))
                {
                    sender.Connect((client.IP == connect.FirstUser) ? connect.SecondUser : connect.FirstUser, 5567); 
                    //sender.Se
                }
            }
        }
    }
}
