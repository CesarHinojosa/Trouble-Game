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
            Guid pieceId = Guid.Parse("44fe7a64-ef08-4f19-9387-dc842c7eabb8");
            Guid gameId = Guid.Parse("3d02117a-4051-460a-ba4d-baf5d4e583be");
            Assert.AreEqual(6, new PieceGameManager(options).MovePiece(pieceId, gameId, 4, true));
        }

        [TestMethod]
        public void SendToHomeTest()
        {
            Guid gameId = Guid.Parse("3d02117a-4051-460a-ba4d-baf5d4e583be");
            Guid pieceId = new PieceGameManager(options).Load().FirstOrDefault(r => r.PieceLocation == 2 && r.GameId == gameId).PieceId;

            int results = new PieceGameManager(options).MovePiece(pieceId, gameId, 3, true);
            Assert.AreEqual(2, results);
        }
    }
}
