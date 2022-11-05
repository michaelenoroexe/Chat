using UI.Connect;
using System.Windows;
using System.Configuration;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChatConnect? connect;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Connect to remoute host.
        /// </summary>
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            string serverIp = ConfigurationManager.AppSettings.Get("ServerIp")!;
            int serverPort = int.Parse(ConfigurationManager.AppSettings.Get("ServerPort")!);

            connect = new ChatConnect(serverIp, serverPort);

            connect.MessageReceived += (s, e) => CWind.Items.Add(e.Message);

            Connect.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Visible;
        }
        
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string message = Message.Text;

            connect!.Send(message);

            Message.Text = "";
        }
    }
}
