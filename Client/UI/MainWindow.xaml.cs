using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable   
    {
        private TcpClient tcpClient = null;

        private Task list;

        private int connkey = -1;

        private object block = new object();

        public MainWindow()
        {
            InitializeComponent();
           
        }

        public void Dispose()
        {
            tcpClient.Dispose();
            list.Dispose();
        }

        private async void Con(object sender, RoutedEventArgs e)
        {
            await Task.Run(ConnectToServer);
            Connect.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Visible;
            list = Task.Run(Listener);
        }

        private void Sen(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private async Task ConnectToServer()
        {
            tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(IPAddress.Parse(ConfigurationManager.AppSettings["Server"]), 5567);
                // Byte to make server add users connection
                var a = new StreamWriter(tcpClient.GetStream());

                a.WriteLine("AAAAAAAAA");

                a.Flush();

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void SendMessage()
        {
            var at = new StreamWriter(tcpClient.GetStream());
            at.WriteLine(Message.Text);
            at.Flush();
            tcpClient.GetStream().Flush();         
        }

        private async Task Listener()
        {
            //TcpListener list = new TcpListener(IPAddress.Parse(ConfigurationManager.AppSettings["Server"]), 5567);
            //list.Start();
            string? st;
            while (true)
            {
                st = new StreamReader(tcpClient.GetStream())?.ReadLine();
                tcpClient.GetStream().Flush();

                if (st is not null)
                {
                    
                    Dispatcher.Invoke(() => AddToList(st));

                }
                //TcpClient c = listener.AcceptTcpClient();
            } 
        }

        private void AddToList(string st)
        {
            CWind.Items.Add(st);
        }
    }
}
