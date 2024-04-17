using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Trouble.BL;
using Trouble.PL.Data;
namespace Trouble.API.Hubs
{
    public class TroubleHub : Hub
    {
        private readonly DbContextOptions<TroubleEntities> options;

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

        }

        public async Task RollDice(string user)
        {
            string message = ("Rolled a " + new GameManager(options).Roll().ToString());
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
