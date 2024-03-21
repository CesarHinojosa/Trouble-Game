using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.BL.Test;

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
        public void DeleteTest()
        {   
            Guid row = new PieceGameManager(options).Load().FirstOrDefault().Id;
            Assert.IsTrue(new PieceGameManager(options).Delete(row, true) > 0);
        }

        [TestMethod]
        public void MovePieceTest()
        {
            Guid pieceId = new PieceManager(options).Load().FirstOrDefault().Id;
            Guid gameId = new GameManager(options).Load().FirstOrDefault().Id;
            Assert.AreEqual(1, new PieceGameManager(options).MovePiece(pieceId, gameId, 4, true));
        }

        [TestMethod]
        public void SendToHomeTest()
        {
            Guid gameId = new GameManager(options).Load().FirstOrDefault().Id;
            Guid pieceId = new PieceGameManager(options).Load().FirstOrDefault(r => r.PieceLocation == 2 && r.GameId == gameId).PieceId;

            int results = new PieceGameManager(options).MovePiece(pieceId, gameId, 3, true);
            Assert.AreEqual(2, results);
        }
    }
}
