using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Trouble.BL.Models;

namespace Trouble.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string hubAddress = "https://localhost:7081/TroubleHub";
        //private string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";
        private HubConnection _connection;
        private int TurnNum = 1;
        private Guid gameId;
        private string user;

        public MainWindow(string username, Guid gameId)
        {
            InitializeComponent();
            user = username;
            this.gameId = gameId;
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            RollDice(user);
        }

        private async void GameStart(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7081/api/PieceGame/" + gameId.ToString();

            //string url = "https://bigprojectapi-300077578.azurewebsites.net/api/PieceGame/";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<List<PieceGame>>(responseBody);

                        int i = 1;

                        foreach(var pieceGame in responseObject ) 
                        {
                            Ellipse piece = (Ellipse)FindName("Piece" + i);

                            //Move pieces location if it is not at start
                            if (pieceGame.PieceLocation != 0) piece.Margin = (Thickness)FindName("Space" + pieceGame.PieceLocation).GetType().GetProperty("Margin").GetValue(FindName("Space" + pieceGame.PieceLocation));

                            //Sets piece specific data onto piece element on UI
                            piece.Resources.Add("PieceId", pieceGame.PieceId.ToString());
                            piece.Resources.Add("PieceLocation", pieceGame.PieceLocation.ToString());
                            piece.Resources.Add("Color", pieceGame.PieceColor.ToString());

                            //piece.GetType().GetProperty("Name").SetValue(piece, responseObject[i].PieceId);

                            i++;
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
        }

        private void MovePieceClick(object sender, MouseButtonEventArgs e)
        {
            Ellipse piece = (Ellipse)sender;

            MovePiece(Guid.Parse(piece.FindResource("PieceId").ToString()), gameId, 1);

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

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));
            _connection.On<Guid, int>("MovePieceReturn", (g1, i1) => MovePieceReturn(g1, i1));
            _connection.StartAsync();
        }

        private void OnSend(string user, object message)
        {
            Console.WriteLine(user + ": " + message);
        }

        private void MovePieceReturn(Guid pieceId, int newLocation)
        {
            for (int i = 1; i < 17; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Ellipse piece = (Ellipse)FindName("Piece" + i);
                    Guid id = Guid.Parse(piece.FindResource("PieceId").ToString());
                    if (id == pieceId)
                    {
                        piece.Resources.Remove("PieceLocation");
                        piece.Resources.Add("PieceLocation", newLocation.ToString());
                        piece.Margin = (Thickness)FindName("Space" + newLocation).GetType().GetProperty("Margin").GetValue(FindName("Space" + newLocation));
                    }
                });
            }
        }
    }
}
