using Microsoft.SqlServer.Server;
using Trouble.BL.Models;
using Trouble.PL.Entities;

namespace Trouble.API.Test
{
    [TestClass]
    public class utGame : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Game>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Game game = new Game { GameName = "TestAPI" };
            await base.InsertTestAsync<Game>(game);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {

            //Game name has to match what is in the API
            await base.DeleteTestAsync1<Game>(new KeyValuePair<string, string>("GameName", "Game2"));
        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<Game>(new KeyValuePair<string, string>("GameName", "Game1"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Game game = new Game { GameName = "Test", GameDate = DateTime.Now, UserColor = "Green" };
            await base.UpdateTestAsync<Game>(new KeyValuePair<string, string>("GameName", "Game1"), game);

        }
    }
}