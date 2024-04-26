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
        string url = "https://localhost:7081/api/Game/GetByUser/";
        List<Game> games = new List<Game>();
        User user;

        public GamesWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            Title = "Games for " + user.Username;
            RebindGames();
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
                Game selectedGmae = dgGames.SelectedItem as Game;
                MainWindow window = new MainWindow(user.Username, selectedGmae);
                window.Show();
            }
        }
    }
}