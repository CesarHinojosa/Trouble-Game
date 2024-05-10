using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for GamesWindow.xaml
    /// </summary>
    public partial class GamesWindow : Window
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
            Title = "Games for " + user.Username;
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
            dgGames.ItemsSource = games;
            dgGames.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void dgGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if(grid != null)
            {
                Game selectedGame = dgGames.SelectedItem as Game;
                MainWindow window = new MainWindow(user.Username, selectedGame);
                window.Show();
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (_connection == null) Start();

            try
            {
                _connection.SendAsync("CreateGame", user.Id);
                _connection.On<Guid>("CreatingGame", (g1) => CreatingGame(g1));
                _connection.On<Game, List<UserGame>>("CreatedGame", (g1, ug1) => CreatedGame(g1, ug1));
                lblCreateGame.Visibility = Visibility.Visible;
                btnComputer.Visibility = Visibility.Hidden;
                btnCreate.Visibility = Visibility.Hidden;
                btnLogOut.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CreatingGame(Guid g1)
        {
            userGuids.Add(g1);
            if(userGuids.Count == 4)
            {
                _connection.SendAsync("CreateTheGame", userGuids);
            }
        }

        private void CreatedGame(Game game, List<UserGame> userGames)
        {
            this.Dispatcher.Invoke(() =>
            {

                lblCreateGame.Visibility = Visibility.Hidden;
                btnComputer.Visibility = Visibility.Visible;
                btnCreate.Visibility = Visibility.Visible;
                btnLogOut.Visibility = Visibility.Visible;
                foreach(UserGame userGame in userGames)
                {
                    if (userGame.UserId == user.Id) game.UserColor = userGame.PlayerColor;
                }
                MainWindow window = new MainWindow(user.Username, game);
                _connection.SendAsync("RemoveFromGroup");
                window.Show();
                RebindGames();
            });
        }

        private void btnComputer_Click(object sender, RoutedEventArgs e)
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
            this.Dispatcher.Invoke(() =>
            {
                MainWindow window = new MainWindow(user.Username, g1);
                window.Show();
                RebindGames();
            });
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
    }
}