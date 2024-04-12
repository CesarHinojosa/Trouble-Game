using Microsoft.AspNetCore.SignalR;
namespace Trouble.API.Hubs
{
    public class TroubleHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

        }

        
    }
}
