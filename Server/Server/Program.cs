
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

            var keyBuff = new byte[4];
            
            //Listening begins
            listener.Start();
            while (true)
            {
                Console.WriteLine("Ожидание подключений... ");

                // Receiving connection
                TcpClient client = listener.AcceptTcpClient();

                //client.Client.RemoteEndPoint?.Serialize();
                try
                {
                    using (var stream = client.GetStream())
                    {
                        stream.Read(keyBuff, 0, keyBuff.Length);

                        Console.WriteLine(stream.ToString());

                        Console.WriteLine(Encoding.Unicode.GetString(keyBuff));
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Empty key string");
                }
            }
        }
    }
}
