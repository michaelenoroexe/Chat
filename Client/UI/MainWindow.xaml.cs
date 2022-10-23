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

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable   
    {
        Stream? stream = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            stream.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(ConnectToServer);
        }

        private async Task ConnectToServer()
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(IPAddress.Loopback, 5567);
                stream = tcpClient.GetStream();
                // Byte to make server add users connection
                stream.WriteByte((byte)194);

                Connect.Visibility = Visibility.Hidden;
                Send.Visibility = Visibility.Visible;
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
    }
}
