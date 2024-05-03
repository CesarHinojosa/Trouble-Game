using Microsoft.AspNetCore.SignalR.Client;
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
using System.Windows.Shapes;
using Trouble.BL.Models;

namespace Trouble.WPFUI
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        private string hubAddress = "https://localhost:7081/TroubleHub";
        //private string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";

        HubConnection _connection = null;

        public CreateUser()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if(firstName != null && lastName != null && username != null && password != null)
            {
                if (_connection == null) Start();

                _connection.InvokeAsync("CreateUser", username, password, firstName, lastName);
            }
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
                    this.Close();
                });
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
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

    }
}
