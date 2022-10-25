
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        private static TcpClient? user = null;
        private static object loc = new object();

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 5567);

            var taskConf = TaskCreationOptions.LongRunning;
            var taskListen = Task.Factory.StartNew(() => Listen(listener), taskConf);
            var taskSend = Task.Factory.StartNew(Write, taskConf);

            Task.WaitAll(taskListen, taskSend);
        }


        private static void Listen(TcpListener listener)
        {
            listener.Start();

            while (true)
            {
                user = listener.AcceptTcpClient();
                Console.WriteLine("UserRecieved");
                break;
            }
        }

        private static void Write()
        {
            Console.ReadLine();
            var userStream = user.GetStream();
            var userStreamReader = new StreamReader(userStream);
            var userStreamWriter = new StreamWriter(userStream);
            string? line;
            while (true)
            {
                line = userStreamReader.ReadLine();
                Console.WriteLine(line);
                

                userStreamWriter.WriteLine(line);

                userStreamWriter.Flush();
            }
        }
    }
}
