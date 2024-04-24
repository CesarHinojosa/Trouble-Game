using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
using Trouble.ConsoleApp;

namespace Trouble.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string hubAddress = "https://localhost:7081/TroubleHub";
        private HubConnection _connection;
        private int TurnNum = 1;
        private string user;

        public MainWindow(string username)
        {
            InitializeComponent();
            user = username;
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            RollDice(user);
        }

        private async void GameStart(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7081/api/PieceGame/";

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
            string hubAddress = "https://localhost:7081/TroubleHub";

            MovePiece(Guid.Parse(piece.FindResource("PieceId").ToString()), Guid.Parse("d20228c1-e0b5-4dc7-b7dd-014414397feb"), 1);
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
                    piece.Resources.Remove("PieceLocation");
                    piece.Resources.Add("PieceLocation", newLocation.ToString());
                    if (id == pieceId)
                    {
                        piece.Margin = (Thickness)FindName("Space" + newLocation).GetType().GetProperty("Margin").GetValue(FindName("Space" + newLocation));
                    }
                });
            }
        }
    }
}
