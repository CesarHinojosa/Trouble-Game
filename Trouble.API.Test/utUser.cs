using Microsoft.SqlServer.Server;
using Trouble.BL.Models;

namespace Trouble.API.Test
{
    [TestClass]
    public class utUser : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<User>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            User user = new User { Username = "Test User" };
            await base.InsertTestAsync<User>(user);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<User>(new KeyValuePair<string, string>("Id", "29a8209c-bc1e-45fb-975a-b3be403c8eb7"));

        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<User>(new KeyValuePair<string, string>("Id", "29a8209c-bc1e-45fb-975a-b3be403c8eb7"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            User user = new User { Username = "Test User", FirstName = "Test", LastName = "Test", Password = "Test" };
            await base.UpdateTestAsync<User>(new KeyValuePair<string, string>("Id", "29a8209c-bc1e-45fb-975a-b3be403c8eb7"), user);

        }
    }
}