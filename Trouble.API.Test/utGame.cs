using Microsoft.SqlServer.Server;
using Trouble.BL.Models;

namespace Trouble.API.Test
{
    [TestClass]
    public class utGame : utBase
    {
        //[TestMethod]
        //public async Task LoadTestAsync()
        //{
        //    await base.LoadTestAsync<Game>();
        //}

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Game game = new Game { GameName = "TestAPI" };
            await base.InsertTestAsync<Game>(game);

        }

        //[TestMethod]
        //public async Task DeleteTestAsync()
        //{
        //    Game game = new Game { GameName = "TestAPI" };
        //    await base.InsertTestAsync<Game>(game);
        //    await base.DeleteTestAsync1<Game>(new KeyValuePair<string, string>("GameName", "TestAPI"));
        //}

        //[TestMethod]
        //public async Task LoadByIdTestAsync()
        //{
        //    await base.LoadByIdTestAsync<Game>(new KeyValuePair<string, string>("GameName", "Other"));
        //}

        //[TestMethod]
        //public async Task UpdateTestAsync()
        //{
        //    Game game = new Game { GameName = "Test" };
        //    await base.UpdateTestAsync<Game>(new KeyValuePair<string, string>("GameName", "Other"), game);

        //}
    }
}