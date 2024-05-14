using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;
using Trouble.BL.Models;

namespace Trouble.MauiUI
{
    public partial class MainPage : ContentPage
    {
        //private string hubAddress = "https://localhost:7081/TroubleHub";
        private string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";

        HubConnection _connection = null;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
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
            _connection.On<bool, User>("LoginResult", (b1, u1) => LoginResult(b1, u1));
            _connection.StartAsync();
        }

        private void OnSend(string user, object message)
        {
            Console.WriteLine(user + ": " + message);
        }

        private async void LoginResult(bool result, User user)
        {
            if (result)
            {
                Dispatcher.Dispatch(() =>
                {
                    this.Title = "Login Successful";
                    GamesWindow games = new GamesWindow(user);
                    this.Navigation.PushAsync(games);
                });
            }
            else
            {
                Dispatcher.Dispatch(() =>
                {
                    DisplayAlert("Login", "Incorrect Username or Password", "OK");
                });
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

        private void btnCreate_Clicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }

}
