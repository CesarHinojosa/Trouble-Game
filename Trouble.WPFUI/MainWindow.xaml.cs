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
using Trouble.ConsoleApp;

namespace Trouble.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string hubAddress = "https://localhost:7081/TroubleHub";
        private int TurnNum = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            string user = "Luke";

            var signalRConnection = new SignalRConnection(hubAddress);

            signalRConnection.RollDice(user);
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

                            if (pieceGame.PieceLocation != 0) piece.Margin = (Thickness)FindName("Space" + pieceGame.PieceLocation).GetType().GetProperty("Margin").GetValue(FindName("Space" + pieceGame.PieceLocation));
                            piece.Resources.Add("PieceId", pieceGame.PieceId.ToString());

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

        private void MovePiece(object sender, MouseButtonEventArgs e)
        {
            Ellipse piece = (Ellipse)sender;
            string user = "Luke";
            string hubAddress = "https://localhost:7081/TroubleHub";

            var signalRConnection = new SignalRConnection(hubAddress);

            signalRConnection.MovePiece(Guid.Parse(piece.FindResource("PieceId").ToString()), Guid.Parse("c225c4f3-f378-467b-9722-7c5852cb584e"), 1);
        }
    }
}
