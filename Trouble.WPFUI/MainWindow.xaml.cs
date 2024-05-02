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
        private bool diceRolled = false;
        private bool gameOver = false;


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
            Start();
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            if(!diceRolled && !gameOver)RollDice(user);
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
                            string color = pieceGame.PieceColor;
                            int location = pieceGame.PieceLocation;

                            //Move pieces location if it is not at start
                            if (location != 0 && location <= 28) piece.Margin = (Thickness)FindName("Space" + location).GetType().GetProperty("Margin").GetValue(FindName("Space" + location));
                            else if (location > 28) piece.Margin = (Thickness)FindName(color + "Home" + (location - 28)).GetType().GetProperty("Margin").GetValue(FindName(color + "Home" + (location - 28)));

                            //Sets piece specific data onto piece element on UI
                            piece.Resources.Add("PieceId", pieceGame.PieceId.ToString());
                            piece.Resources.Add("PieceLocation", location.ToString());
                            piece.Resources.Add("Color", color);

                            //piece.GetType().GetProperty("Name").SetValue(piece, responseObject[i].PieceId);

                            i++;
                        }

                        
                        // Handle unsuccessful response
                        Console.WriteLine("Failed to call the API. Status code: " + response.StatusCode);
                    }
                    lblDirections.Content = TurnNum.ToString() + " Player, Roll the Dice";
                    lblTurn.Content = "Turn: " + TurnNum.ToString();
                    if (CheckForWin("Green") || CheckForWin("Yellow") || CheckForWin("Blue") || CheckForWin("Red")) gameOver = true;

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

            if (piece.FindResource("Color").ToString() == TurnNum.ToString() && diceRolled && !gameOver)
            {

                MovePiece(Guid.Parse(piece.FindResource("PieceId").ToString()), game.Id, lastRoll);
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
            _connection.On<int>("DiceRolled", (i1) => {
                lastRoll = i1;
                diceRolled = true;
                this.Dispatcher.Invoke(() =>
                {
                    lblRoll.Content = i1;
                });
                });
            _connection.StartAsync();
        }

        private void OnSend(string user, object message)
        {
            Console.WriteLine(user + ": " + message);
        }

        private void MovePieceReturn(Guid pieceId, int newLocation)
        {
            bool pieceMoved = false;

            for (int i = 1; i < 17; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Ellipse piece = (Ellipse)FindName("Piece" + i);
                    Guid id = Guid.Parse(piece.FindResource("PieceId").ToString());
                if (id == pieceId && newLocation != 0)
                {
                    if (piece.FindResource("PieceLocation") != newLocation.ToString())
                    {
                        pieceMoved = true;
                        diceRolled = false;
                    }

                    if (newLocation <= 28)
                    {
                        piece.Resources.Remove("PieceLocation");
                        piece.Resources.Add("PieceLocation", newLocation.ToString());
                        piece.Margin = (Thickness)FindName("Space" + newLocation).GetType().GetProperty("Margin").GetValue(FindName("Space" + newLocation));
                    }
                    else
                    {
                        string color = piece.FindResource("Color").ToString();
                        piece.Resources.Remove("PieceLocation");
                        piece.Resources.Add("PieceLocation", newLocation.ToString());
                        piece.Margin = (Thickness)FindName(color + "Home" + (newLocation - 28)).GetType().GetProperty("Margin").GetValue(FindName(color + "Home" + (newLocation - 28)));
                        if (CheckForWin(color)) gameOver = true;
                    }
                }
                else if (int.Parse(piece.FindResource("PieceLocation").ToString()) == 0 && newLocation == 0)
                    {
                        pieceMoved = true;
                        diceRolled = false;
                    }
                });
            }
            this.Dispatcher.Invoke(() =>
            {

                if (lastRoll != 6 && pieceMoved)
                {

                    TurnNum++;
                    if (TurnNum > (Color)3)
                    {
                        TurnNum = 0;
                    }
                    lblDirections.Content = TurnNum.ToString() + " Player, Roll the Dice";
                    lblTurn.Content = "Turn: " + TurnNum.ToString();
                }
                else if (lastRoll == 6)
                {
                    lblDirections.Content = "Roll the Dice Again";
                }
                else
                {
                    lblDirections.Content = "Select a Different Piece";
                }
            });
        }

        private bool CheckForWin(string color)
        {
            int counter;
            int max;

            if(color == "Green")
            {
                counter = 1;
                max = 5;
            }
            else if (color == "Yellow")
            {
                counter = 5;
                max = 9;
            }
            else if (color == "Blue")
            {
                counter = 9;
                max = 13;
            }
            else
            {
                counter = 13;
                max = 17;
            }

            for(; counter < max; counter++)
            {
                Ellipse piece = (Ellipse)FindName("Piece" + counter);
                int location = int.Parse(piece.FindResource("PieceLocation").ToString());
                if (location < 28) return false;
            }

            lblDirections.Content = color + " Wins!";
            return true;
        }
    }
}
