
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        private static Queue<UserRequest> _reqQueue = new Queue<UserRequest>();

        private static Dictionary<int, Connection> _connStrings = new Dictionary<int, Connection>();

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
                    connect.Send(client);
                    Console.WriteLine("Resend Data");
                    continue;
                }                
                //Create connection and send in to clients
                if (_reqQueue.Any())
                {
                    // Return if client already in queue
                    if (_reqQueue.Peek().GetHashCode() == client.GetHashCode()) continue;

                    RegisterConnection(_reqQueue.Dequeue(), client);
                    Console.WriteLine("Get from queue");
                    continue;
                }
                //Add new client to queue
                _reqQueue.Enqueue(client);
                Console.WriteLine("Set to queue");
            }
        }

        private static async Task RegisterConnection(UserRequest firstUser, UserRequest secondUser)
        {
            var con = new Connection(firstUser, secondUser);

            _connStrings.Add(con.GetHashCode(), con);
            //firstUser.GetStream().Write(BitConverter.GetBytes(con.GetHashCode()));
            //secondUser.GetStream().Write(BitConverter.GetBytes(con.GetHashCode()));
        }
    }
}
