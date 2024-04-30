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
        private enum Color
        {
            Green = 0,
            Yellow = 1,
            Blue = 2,
            Red = 3
        };
        private Color TurnNum;
        private int lastRoll;
        private Game game;
        private string user;


        private static int ComparePieceColor(PieceGame x, PieceGame y)
        {
            Enum.TryParse(x.PieceColor, out Color color1);
            Enum.TryParse(y.PieceColor, out Color color2);
            if (color1 == color2) return 0;
            else if (color1 > color2) return 1;
            else return -1;
        }

        public MainWindow(string username, Game game)
        {
            InitializeComponent();
            user = username;
            this.game = game;
            TurnNum = (Color)game.TurnNum;
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            RollDice(user);
        }

        private async void GameStart(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7081/api/PieceGame/" + game.Id.ToString();

            //string url = "https://bigprojectapi-300077578.azurewebsites.net/api/PieceGame/" + game.Id.ToString();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<List<PieceGame>>(responseBody);

                        responseObject.Sort(ComparePieceColor);

                        int i = 1;

                        foreach (var pieceGame in responseObject)
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
                    lblDirections.Content = TurnNum.ToString() + " Player, Roll the Dice";
                    lblTurn.Content = "Turn: " + TurnNum.ToString();
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

            if (piece.FindResource("Color").ToString() == TurnNum.ToString())
            {
                MovePiece(Guid.Parse(piece.FindResource("PieceId").ToString()), game.Id, 1);
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
            _connection.On<int>("DiceRolled", (i1) => lastRoll = i1);
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
            this.Dispatcher.Invoke(() =>
            {

                if (lastRoll != 6)
                {

                    TurnNum++;
                    if (TurnNum > (Color)3)
                    {
                        TurnNum = 0;
                    }
                    lblDirections.Content = TurnNum.ToString() + " Player, Roll the Dice";
                    lblTurn.Content = "Turn: " + TurnNum.ToString();
                }
                else
                {
                    lblDirections.Content = "Roll the Dice Again";
                }
            });
        }
    }
}
