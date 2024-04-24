using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Trouble.BL;
using Trouble.BL.Models;
using Trouble.PL.Data;
namespace Trouble.API.Hubs
{
    public class TroubleHub : Hub
    {
        private readonly DbContextOptions<TroubleEntities> options;
        private readonly UserManager UserManager;

        public TroubleHub(DbContextOptions<TroubleEntities> options)
        {
            this.options = options;
            this.UserManager = new UserManager(options);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

        }

        public async Task RollDice(string user)
        {
            string message = ("Rolled a " + new GameManager(options).Roll().ToString());
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task MovePiece(Guid pieceId, Guid gameId, int spaces)
        {
            PieceGameManager pieceGameManager = new PieceGameManager(options);
            pieceGameManager.MovePiece(pieceId, gameId, spaces);
            string message = "Moved piece " + pieceId + " " + spaces + " spaces";
            await Clients.All.SendAsync("ReceiveMessage", "System", message);

        }

        public async Task Login(string username, string password)
        {

            User user = new User { Username = username, Password = password };
            bool loginResult = UserManager.Login(user);

            await Clients.All.SendAsync("ReceiveMessage", username, "Login Successful");
        }

        public async Task Logout(string username)
        {
            try
            {
                User user = new User
                {
                    Username = username,
                };

                new UserManager(options).Logout(user);

                await Clients.All.SendAsync("ReceiveMessage", username, "Logout Successful");
            }
            catch (Exception ex) 
            {
                await Clients.Caller.SendAsync("ReceiveMessage", username, "Error occurred during logout: " + ex.Message);
            }
        }
        
    }
}
