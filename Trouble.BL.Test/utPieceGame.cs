using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.Bl.Test;

namespace Trouble.BL.Test
{
    [TestClass]
    public class utPieceGame : utBase
    {
        [TestMethod]
        public void InsertTest()
        {


            Game game = new GameManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new GameManager(options).LoadById(game.Id).Id, game.Id);

            Piece piece = new PieceManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new PieceManager(options).LoadById(piece.Id).Id, piece.Id);

            int result = new PieceGameManager(options).Insert(piece.Id, game.Id, true);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            ////Loads the first Game and changes the game name
            //Game game = new GameManager(options).Load().FirstOrDefault();
            //game.GameName = "New Game";

            ////loads the first piece and changes the color
            //Piece piece = new PieceManager(options).Load().FirstOrDefault();
            ////piece.Color = "Brownish";

            //Assert.IsTrue(new PieceGameManager(options).Update(piece.Id, game.Id, true) > 0);


            //Piece piece = new PieceManager(options).Load().FirstOrDefault();

            //piece.Color = "BrownIsh";

            //tblPieceGame pieceGame = new PieceGameManager(options).Load()FirstOrDefault();

        }

        [TestMethod]
        public void DeleteTest()
        {
            //Piece piece = new PieceManager(options).Load().FirstOrDefault();

            //Game game = new GameManager(options).Load().FirstOrDefault(x => x.GameName == "Game2");


            //Assert.IsTrue(new PieceGameManager(options).Delete(game.Id, true) > 0);
        }
    }
}
