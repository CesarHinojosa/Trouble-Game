using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using Trouble.BL.Models;

namespace Trouble.MauiUI;

public partial class MainWindow : ContentPage
{
    //private string hubAddress = "https://localhost:7081/TroubleHub";
    private string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";
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
    private bool computerGame = false;

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
        if (game.GameName == "ComputerGame") computerGame = true;
        user = username;
        this.game = game;
        TurnNum = (Color)game.TurnNum;
        lblUser.Text = "Color: " + game.UserColor;
        Start();
        _connection.SendAsync("JoinGame", user, game.Id);
    }

    private void btnRoll_Click(object sender, EventArgs e)
    {
        if (!diceRolled && !gameOver && game.UserColor == TurnNum.ToString()) RollDice(user);
    }

    private async void GameStart(object sender, EventArgs e)
    {
        //string url = "https://localhost:7081/api/PieceGame/" + game.Id.ToString();

        string url = "https://bigprojectapi-300077578.azurewebsites.net/api/PieceGame/" + game.Id.ToString();

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
                        Ellipse piece = (Ellipse)FindByName("Piece" + i);
                        string color = pieceGame.PieceColor;
                        int location = pieceGame.PieceLocation;

                        //Move pieces location if it is not at start
                        if (location != 0 && location <= 28) piece.Margin = (Thickness)FindByName("Space" + location).GetType().GetProperty("Margin").GetValue(FindByName("Space" + location));
                        else if (location > 28) piece.Margin = (Thickness)FindByName(color + "Home" + (location - 28)).GetType().GetProperty("Margin").GetValue(FindByName(color + "Home" + (location - 28)));

                        //Sets piece specific data onto piece element on UI
                        piece.Resources.Add("PieceId", pieceGame.PieceId.ToString());
                        piece.Resources.Add("PieceLocation", location.ToString());
                        piece.Resources.Add("Color", color);
                        piece.Resources.Add("PieceNum", i);

                        //piece.GetType().GetProperty("Name").SetValue(piece, responseObject[i].PieceId);

                        i++;
                    }


                    // Handle unsuccessful response
                    Console.WriteLine("Failed to call the API. Status code: " + response.StatusCode);
                }
                lblDirections.Text = TurnNum.ToString() + " Player, Roll the Dice";
                lblTurn.Text = "Turn: " + TurnNum.ToString();
                if (CheckForWin("Green") || CheckForWin("Yellow") || CheckForWin("Blue") || CheckForWin("Red")) gameOver = true;
                if (computerGame && TurnNum.ToString() != game.UserColor)
                {
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        ComputerTurn();
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    private void MovePieceClick(object sender, TappedEventArgs e)
    {
        Ellipse piece = (Ellipse)sender;
        piece.Resources.TryGetValue("Color", out object pieceColor);

        if (pieceColor.ToString() == TurnNum.ToString() && diceRolled && !gameOver && game.UserColor == pieceColor.ToString())
        {

            piece.Resources.TryGetValue("PieceId", out object pieceId);
            MovePiece(Guid.Parse(pieceId.ToString()), game.Id, lastRoll);
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
            _connection.InvokeAsync("RollDice", user, game.Id);
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
        _connection.On<Guid>("ComputerReturn", (g1) => MovePiece(g1, game.Id, lastRoll));
        _connection.On<string>("ComputerMoveFail", (s1) => NextTurn());
        _connection.On<int>("Skip", (i1) => NextTurn());
        _connection.On<int>("DiceRolled", (i1) => {
            lastRoll = i1;
            diceRolled = true;
            Dispatcher.Dispatch(() =>
            {
                lblRoll.Text = i1.ToString();
                lblDirections.Text = TurnNum.ToString() + " player, Select a Piece to Move";
            });
            if (computerGame && TurnNum.ToString() != game.UserColor)
            {
                Task.Delay(2000).ContinueWith(_ =>
                {
                    _connection.InvokeAsync("ComputerTurn", game.Id, TurnNum.ToString(), lastRoll);
                });
            }
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

        for (int i = 1; i < 16; i++)
        {
            Dispatcher.Dispatch(() =>
            {
                Ellipse piece = (Ellipse)FindByName("Piece" + i);
                piece.Resources.TryGetValue("PieceId", out object pieceid);
                Guid id = Guid.Parse(pieceid.ToString());
                piece.Resources.TryGetValue("PieceLocation", out object location);
                if (id == pieceId && newLocation != 0)
                {
                    if (location.ToString() != newLocation.ToString())
                    {
                        pieceMoved = true;
                        diceRolled = false;
                    }

                    if (newLocation <= 28)
                    {
                        Dispatcher.Dispatch(() =>
                        {
                            piece.Resources.Remove("PieceLocation");
                            piece.Resources.Add("PieceLocation", newLocation.ToString());
                            piece.Margin = (Thickness)FindByName("Space" + newLocation).GetType().GetProperty("Margin").GetValue(FindByName("Space" + newLocation));

                        });
                    }
                    else
                    {
                        Dispatcher.Dispatch(() =>
                        {
                            piece.Resources.TryGetValue("Color", out object color);
                            piece.Resources.Remove("PieceLocation");
                            piece.Resources.Add("PieceLocation", newLocation.ToString());
                            piece.Margin = (Thickness)FindByName(color.ToString() + "Home" + (newLocation - 28)).GetType().GetProperty("Margin").GetValue(FindByName(color + "Home" + (newLocation - 28)));
                            if (CheckForWin(color.ToString())) gameOver = true;
                        });
                    }

                }

                //Check if piece is on location but is not the piece being moved

                else if (id != pieceId && newLocation == int.Parse(location.ToString()))
                {
                    //If the piece is not at home base or start
                    if (newLocation <= 28 && newLocation > 0)
                    {
                        Dispatcher.Dispatch(() =>
                        {
                            piece.Resources.TryGetValue("Color", out object color);
                            piece.Resources.Remove("PieceLocation");
                            piece.Resources.Add("PieceLocation", 0.ToString());
                            piece.Resources.TryGetValue("PieceNum", out object num);
                            //Move the piece to starting position
                            piece.Margin = (Thickness)FindByName("Piece" + num.ToString() + "Home").GetType().GetProperty("Margin").GetValue(FindByName("Piece" + num.ToString() + "Home"));
                        });                        
                    }
                }

            });
        }
        if (lastRoll != 6 && pieceMoved && gameOver != true)
        {
            NextTurn();
        }
        else if (lastRoll == 6 && gameOver != true)
        {
            Dispatcher.Dispatch(() => lblDirections.Text = "Roll the Dice Again");
            if (computerGame && TurnNum.ToString() != game.UserColor)
            {
                Task.Delay(2000).ContinueWith(_ =>
                {
                    ComputerTurn();
                });
            }
        }
        else if (gameOver != true)
        {
            Dispatcher.Dispatch(() => lblDirections.Text = "Select a Different Piece");
        }
    }

    private void NextTurn()
    {
        Dispatcher.Dispatch(() =>
        {
            TurnNum++;
            if (TurnNum > (Color)3)
            {
                TurnNum = 0;
            }
            if (diceRolled) diceRolled = false;
            lblDirections.Text = TurnNum.ToString() + " Player, Roll the Dice";
            lblTurn.Text = "Turn: " + TurnNum.ToString();
            if (computerGame && TurnNum.ToString() != game.UserColor) ComputerTurn();

        });
    }

    private bool CheckForWin(string color)
    {
        int counter;
        int max;

        if (color == "Green")
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

        for (; counter < max; counter++)
        {
            Ellipse piece = (Ellipse)FindByName("Piece" + counter);
            piece.Resources.TryGetValue("PieceLocation", out object location);
            if (int.Parse(location.ToString()) < 28) return false;
        }

        lblDirections.Text = color + " Wins!";
        return true;
    }

    private void ComputerTurn()
    {
        if (_connection == null)
        {
            Start();
        }

        try
        {
            _connection.InvokeAsync("RollDice", "Computer", game.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void LeaveGame(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _connection.SendAsync("LeaveGame", user, game.Id);
    }

    private void btnSkip_Click(object sender, EventArgs e)
    {
        if (TurnNum.ToString() == game.UserColor)
        {
            if (lastRoll != 6 && gameOver != true)
            {
                _connection.SendAsync("SkipTurn", game.Id);
            }
            else if (lastRoll == 6 && gameOver != true)
            {
                lblDirections.Text = "Roll the Dice Again";
                if (computerGame && TurnNum.ToString() != game.UserColor)
                {
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        ComputerTurn();
                    });
                }
            }
        }
    }
}