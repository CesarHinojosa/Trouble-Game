using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Trouble.BL.Models;

namespace Trouble.MauiUI;

public partial class GamesWindow : ContentPage
{
    //private string hubAddress = "https://localhost:7081/TroubleHub";
    private string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";

    HubConnection _connection = null;

    //string url = "https://localhost:7081/api/Game/GetByUser/";
    string url = "https://bigprojectapi-300077578.azurewebsites.net/api/Game/GetByUser/";
    List<Game> games = new List<Game>();
    User user;
    List<Guid> userGuids = new List<Guid>();

    public GamesWindow(User user)
	{
		InitializeComponent();
        this.user = user;
        lblGame.Text = "Games for " + user.Username;
        RebindGames();
        Start();
    }

    private async void RebindGames()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + user.Id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<List<Game>>(responseBody);


                    foreach (var game in responseObject)
                    {
                        games.Add(game);
                    }

                    // Handle unsuccessful response
                    Console.WriteLine("Failed to call the API. Status code: " + response.StatusCode);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        dgGames.BindingContext = this;
        dgGames.ItemsSource = games;

        //dgGames.Columns[0].Visibility = Visibility.Collapsed;
    }

    private void CreatingGame(Guid g1)
    {
        userGuids.Add(g1);
        if (userGuids.Count == 4)
        {
            _connection.SendAsync("CreateTheGame", userGuids);
        }
    }

    private void CreatedGame(Game game, List<UserGame> userGames)
    {
        Dispatcher.Dispatch(() =>
        {
            lblCreateGame.IsVisible = false;
            btnComputer.IsVisible = true;
            btnCreate.IsVisible = true;
            btnLogOut.IsVisible = true;

            foreach (UserGame userGame in userGames)
            {
                if (userGame.UserId == user.Id) game.UserColor = userGame.PlayerColor;
            }
            RebindGames();
            MainWindow window = new MainWindow(user.Username, game);
            _connection.SendAsync("RemoveFromGroup");
            ///this.Navigation.PopAsync();
            this.Navigation.PushAsync(window);
        });
    }

    public void Start()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(hubAddress)
            .Build();

        _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));
        _connection.On<Game>("CreateComputer", (g1) => CreateComputer(g1));
        _connection.StartAsync();
    }

    private void CreateComputer(Game g1)
    {
        RebindGames();
        g1.UserColor = "Green";
        MainWindow window = new MainWindow(user.Username, g1);
        this.Navigation.PopAsync();
        this.Navigation.PushAsync(window);
    }

    private void OnSend(string user, object message)
    {
        Console.WriteLine(user + ": " + message);
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

    private void dgGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        CollectionView grid = (CollectionView)sender;
        if (grid != null)
        {
            Game selectedGame = dgGames.SelectedItem as Game;
            MainWindow window = new MainWindow(user.Username, selectedGame);
            this.Navigation.PushAsync(window);
        }
    }

    private void btnLogOut_Click(object sender, EventArgs e)
    {
        LoginPage login = new LoginPage();
        this.Navigation.PopAsync();
        //this.Navigation.PushModalAsync(login);
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        if (_connection == null) Start();

        try
        {
            _connection.SendAsync("CreateGame", user.Id);
            _connection.On<Guid>("CreatingGame", (g1) => CreatingGame(g1));
            _connection.On<Game, List<UserGame>>("CreatedGame", (g1, ug1) => CreatedGame(g1, ug1));
            lblCreateGame.IsVisible = true;
            btnComputer.IsVisible = false;
            btnCreate.IsVisible = false;
            btnLogOut.IsVisible = false;
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void btnComputer_Click(object sender, EventArgs e)
    {
        if (_connection == null) Start();

        try
        {
            _connection.SendAsync("StartComputer", user.Id);
        }
        catch (Exception)
        {

            throw;
        }
    }
}