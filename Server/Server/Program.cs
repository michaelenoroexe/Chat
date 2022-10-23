
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

        private static Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            // Preparing sockets to listen 
            var listener = new TcpListener(IPAddress.Any, 5567);
            
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
                    sender.SendToAsync(client.SendedTextBytes, SocketFlags.None, new IPEndPoint(client.IP, 5567));
                    Console.WriteLine("Resend Data");
                    continue;
                }
                // Return if client already in queue
                if (_reqQueue.Peek().GetHashCode() == client.IP.GetHashCode()) continue;
                //Create connection and send in to clients
                if (_reqQueue.Any())
                {
                    RegisterConnection(_reqQueue.Dequeue(), client.IP);
                    Console.WriteLine("Get from queue");
                    continue;
                }
                //Add new client to queue
                _reqQueue.Enqueue(client.IP);
                Console.WriteLine("Set to queue");
            }
        }

        private static async Task RegisterConnection(IPAddress firstUser, IPAddress secondUser)
        {
            var con = new Connection(firstUser, secondUser);

            _connStrings.Add(con.GetHashCode(), con);
            var sendUs1 = sender.SendToAsync(BitConverter.GetBytes(con.GetHashCode()), SocketFlags.None, new IPEndPoint(firstUser, 5567));
            var sendUs2 = sender.SendToAsync(BitConverter.GetBytes(con.GetHashCode()), SocketFlags.None, new IPEndPoint(secondUser, 5567));

            await sendUs1;
            await sendUs2;
        }
    }
}
