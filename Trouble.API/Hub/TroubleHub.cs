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
            int roll = new GameManager(options).Roll();
            string message = ("Rolled a " + roll.ToString());
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.All.SendAsync("DiceRolled", roll);
        }

        public async Task MovePiece(Guid pieceId, Guid gameId, int spaces)
        {
            PieceGameManager pieceGameManager = new PieceGameManager(options);
            int location = pieceGameManager.MovePiece(pieceId, gameId, spaces);
            string message = "Moved piece " + pieceId + " " + spaces + " spaces";
            await Clients.All.SendAsync("ReceiveMessage", "System", message);
            await Clients.All.SendAsync("MovePieceReturn", pieceId, location);

        }

        public async Task Login(string username, string password)
        {
            try
            {
                User user = new User { Username = username, Password = password };
                bool loginResult = UserManager.Login(user);

                if (loginResult)
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", username, "Login Successful");
                    await Clients.Caller.SendAsync("LoginResult", loginResult, user);
                }
                else
                {
                    //We dont get in here because LoginFailureException executes first
                    //We probably don't need this
                    await Clients.Caller.SendAsync("ReceiveMessage", username, "Login Failed: Incorrect username or password");
                    await Clients.Caller.SendAsync("LoginResult", loginResult, username);
                }
            }
            catch (LoginFailureException ex)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", username, "Login Failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", username, "Error occurred during login: " + ex.Message);
            }
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
