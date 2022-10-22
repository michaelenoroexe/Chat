
using System.Collections;
using System.Net;
using System.Net.Sockets;

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
            
            //Listening begins
            listener.Start();
            while (true)
            {
                Console.WriteLine("Ожидание подключений... ");

                // получаем входящее подключение
                TcpClient client = listener.AcceptTcpClient();

                client.Client.RemoteEndPoint?.Serialize();
            }
        }
    }
}
