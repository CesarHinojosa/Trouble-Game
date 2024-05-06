using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;
using Trouble.BL.Models;

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
            string password = txtPassword.Password;

            Login(username, password);
        }

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));
            _connection.On<bool, User>("LoginResult", (b1, u1) => LoginResult(b1, u1));
            _connection.StartAsync();
        }

        private void OnSend(string user, object message)
        {
            Console.WriteLine(user + ": " + message);
        }

        private void LoginResult(bool result, User user)
        {
            if (result)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Title = "Login Successful";
                    GamesWindow games = new GamesWindow(user);
                    games.Show();
                    //this.Close();
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                CreateUser createUser = new CreateUser();
                createUser.Show();
                this.Close();
            });
        }
    }
}
