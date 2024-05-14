using Microsoft.AspNetCore.SignalR.Client;
using Trouble.BL.Models;

namespace Trouble.MauiUI;

public partial class CreateUser : ContentPage
{
    //private string hubAddress = "https://localhost:7081/TroubleHub";
    private string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";

    HubConnection _connection = null;

    public CreateUser()
	{
        InitializeComponent();
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        string firstName = txtFirstName.Text;
        string lastName = txtLastName.Text;
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        if (firstName != null && lastName != null && username != null && password != null)
        {
            if (_connection == null) Start();

            _connection.InvokeAsync("CreateUser", username, password, firstName, lastName);
        }
    }

    private void LoginResult(bool result, User user)
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
                DisplayAlert("Create User Fail", "Failed to Create User Try Again", "OK");
            });
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