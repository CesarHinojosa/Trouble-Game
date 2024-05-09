using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
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

        public async Task RollDice(string user, Guid gameId)
        {
            int roll = new GameManager(options).Roll();
            string message = ("Rolled a " + roll.ToString());
            await Clients.Group(gameId.ToString()).SendAsync("ReceiveMessage", user, message);
            await Clients.Group(gameId.ToString()).SendAsync("DiceRolled", roll);
        }

        public async Task MovePiece(Guid pieceId, Guid gameId, int spaces)
        {
            PieceGameManager pieceGameManager = new PieceGameManager(options);
            int location = pieceGameManager.MovePiece(pieceId, gameId, spaces);
            string message = "Moved piece " + pieceId + " " + spaces + " spaces";
            await Clients.Group(gameId.ToString()).SendAsync("ReceiveMessage", "System", message);
            await Clients.Group(gameId.ToString()).SendAsync("MovePieceReturn", pieceId, location);

        }

        public async Task SkipTurn(Guid gameId)
        {
            GameManager gameManager = new GameManager(options);
            Game game = gameManager.LoadById(gameId);
            game.TurnNum++;
            if (game.TurnNum > 3) game.TurnNum = 0;
            gameManager.Update(game);
            await Clients.Group(gameId.ToString()).SendAsync("Skip", game.TurnNum);
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
                    await Clients.Caller.SendAsync("LoginResult", loginResult, user);
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

                await Clients.Caller.SendAsync("ReceiveMessage", username, "Logout Successful");
            }
            catch (Exception ex) 
            {
                await Clients.Caller.SendAsync("ReceiveMessage", username, "Error occurred during logout: " + ex.Message);
            }
        }

        public async Task CreateUser(string username, string password, string firstName, string lastName)
        {
            try
            {
                User user = new User { Username = username, Password = password, FirstName = firstName, LastName = lastName };
                int create = UserManager.Insert(user);
                if (create > 0)
                {
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
                else
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", username, "Create Failed: Failed to Create User");
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

        public async Task StartComputer(Guid userId)
        {
            try
            {
                Game game = new Game { TurnNum = 0, GameName = "ComputerGame" };
                GameManager gameManager = new GameManager(options);
                gameManager.Insert(game);

                UserGameManager userGameManager = new UserGameManager(options);
                userGameManager.Insert(userId, game.Id, "Green");

                PieceManager pieceManager = new PieceManager(options);
                PieceGameManager pieceGameManager = new PieceGameManager(options);
                List<Piece> pieces = pieceManager.Load();
                foreach (Piece piece in pieces)
                {
                    pieceGameManager.Insert(piece.Id, game.Id);
                }
                await Clients.Caller.SendAsync("CreateComputer", game);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateGame(Guid userId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "CreatingGame");
                await Clients.Group("CreatingGame").SendAsync("CreatingGame", userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateTheGame(List<Guid> userIds)
        {
            try
            {
                GameManager gameManager = new GameManager(options);
                Game game = new Game { TurnNum = 0, GameName = "Game" + (gameManager.Load().Count + 1) };
                gameManager.Insert(game);

                UserGameManager userGameManager = new UserGameManager(options);
                int i = 1;
                string color = "Green";
                List<UserGame> userGames = new List<UserGame>();
                foreach (Guid userId in userIds)
                {
                    if (i == 1) color = "Green";
                    else if (i == 2) color = "Yellow";
                    else if (i == 3) color = "Blue";
                    else if (i == 4) color = "Red";
                    userGameManager.Insert(userId, game.Id, color);
                    UserGame userGame = new UserGame { GameId = game.Id, UserId = userId, PlayerColor = color };
                    userGames.Add(userGame);
                    i++;
                }

                PieceManager pieceManager = new PieceManager(options);
                PieceGameManager pieceGameManager = new PieceGameManager(options);
                List<Piece> pieces = pieceManager.Load();
                foreach (Piece piece in pieces)
                {
                    pieceGameManager.Insert(piece.Id, game.Id);

                }
                await Clients.Group("CreatingGame").SendAsync("CreatedGame", game, userGames);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ComputerTurn(Guid gameId, string color, int spaces)
        {
            try
            {
                PieceGameManager pieceGameManager = new PieceGameManager(options);
                PieceGame pieceGame = pieceGameManager.ComputerMovePiece(gameId, color, spaces);

                if (pieceGame != null) await Clients.Group(gameId.ToString()).SendAsync("ComputerReturn", pieceGame.PieceId);
                else await Clients.Group(gameId.ToString()).SendAsync("ComputerMoveFail", "");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task JoinGame(string playerName, Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

            await Clients.Group(gameId.ToString()).SendAsync("ReceiveMessage", "Game", playerName + " Joined the Game.");
        }

        public async Task LeaveGame(string playerName, Guid gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        }

        public async Task RemoveFromGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "CreatingGame");
        }
    }
}
