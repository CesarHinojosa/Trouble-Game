using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trouble.WPFUI;

namespace Trouble.ConsoleApp
{
    internal class SignalRConnection
    {
        private string hubAddress;
        HubConnection _connection;
        string user;
        Window window;

        public SignalRConnection(string hubAddress, Window window)
        {
            this.hubAddress = hubAddress;
            this.window = window;
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
                user = username;
                window.Title = username;
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

        public void RollDice(string user)
        {
            if (_connection == null)
            {
                Start();
            }

            try
            {
                _connection.InvokeAsync("RollDice", user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void MovePiece(Guid pieceId, Guid gameId, int spaces)
        {
            if (_connection == null)
            {
                Start();
            }

            try
            {
                _connection.InvokeAsync("MovePiece", pieceId, gameId, spaces);
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
