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
        private Stream? stream = null;

        private int connkey = -1;

        public MainWindow()
        {
            InitializeComponent();

            Listener(CWind.Items.Add);
        }

        public void Dispose()
        {
            stream.Dispose();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(ConnectToServer);

            Connect.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Visible;
        }

        private async Task ConnectToServer()
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(IPAddress.Parse(ConfigurationManager.AppSettings["Server"]), 5567);
                stream = tcpClient.GetStream();
                // Byte to make server add users connection
                stream.WriteByte((byte)194);

            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task SendMessage()
        {
            //using (var stream = tcpClient.GetStream())
            //{
            //    stream.WriteByte((byte)194);
            //}
        }

        private async Task Listener(Func<object, int> act)
        {
            TcpListener tcplist = new TcpListener(IPAddress.Parse(ConfigurationManager.AppSettings["Server"]), 5567);

            
        }
    }
}
