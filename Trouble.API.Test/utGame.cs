using Microsoft.SqlServer.Server;
using Trouble.BL.Models;

namespace Trouble.API.Test
{
    [TestClass]
    public class utGame : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Format>();
        }
    }
}