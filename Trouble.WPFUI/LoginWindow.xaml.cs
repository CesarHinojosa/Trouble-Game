using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;

namespace Trouble.WPFUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string hubAddress = "https://localhost:7081/TroubleHub";
        //private string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";

        HubConnection _connection = null;

        public LoginWindow()
        {
            InitializeComponent();
            Start();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            Login(username, password);
        }

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));
            _connection.On<bool, string>("LoginResult", (b1, s1) => LoginResult(b1, s1));
            _connection.StartAsync();
        }

        private void OnSend(string user, object message)
        {
            Console.WriteLine(user + ": " + message);
        }

        private void LoginResult(bool result, string username)
        {
            if (result)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Title = "Login Successful";
                    GamesWindow games = new GamesWindow(username);
                    games.Show();
                    this.Close();
                });
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
        }

        public void ConnectToChannel(string user)
        {
            Start();
            string message = user + " Connected";
            try
            {
                _connection.InvokeAsync("SendMessage", "System", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Login(string username, string password)
        {
            if (_connection == null) Start();

            try
            {
                _connection.InvokeAsync("Login", username, password);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
