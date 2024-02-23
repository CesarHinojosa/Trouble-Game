using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.PL.Entities;

namespace Trouble.PL.Test
{
    [TestClass]
    public class utPieceGame : utBase<tblPieceGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 16;
            var piecegame = base.LoadTest();
            Assert.AreEqual(expected, piecegame.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPieceGame piecegGame = new tblPieceGame();

            piecegGame.Id = Guid.NewGuid();
            piecegGame.GameId = base.LoadTest().FirstOrDefault().GameId;
            piecegGame.PieceId = base.LoadTest().FirstOrDefault().PieceId;
            piecegGame.PieceLocation = 5;

            int results = base.InsertTest(piecegGame);
            Assert.AreEqual(1, results);

        }

        [TestMethod]
        public void UpdateTest()
        {
            tblPieceGame pieceGame = base.LoadTest().FirstOrDefault();

            if(pieceGame != null) 
            {
                pieceGame.PieceLocation = 14;
                pieceGame.GameId = base.LoadTest().FirstOrDefault().GameId;
                pieceGame.PieceId = base.LoadTest().FirstOrDefault().PieceId;


                int rowsAffected = UpdateTest(pieceGame);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblPieceGame pieceGame = base.LoadTest().FirstOrDefault();

            if (pieceGame != null)
            {
                int rowsAffected = DeleteTest(pieceGame);
                Assert.IsTrue(rowsAffected == 1);

            }

        }
    }
}
