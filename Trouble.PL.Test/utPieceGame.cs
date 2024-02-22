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
            int expected = 4;
            var piecegame = base.LoadTest();
            Assert.AreEqual(expected, piecegame.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPieceGame piecegGame = new tblPieceGame();

            piecegGame.Id = Guid.NewGuid();
            piecegGame.GameId = Guid.NewGuid();
            piecegGame.PieceId = Guid.NewGuid();
            piecegGame.PieceLocation = 5;

            TestCleanup();

        }

        [TestMethod]
        public void UpdateTest()
        {
            tblPieceGame pieceGame = base.LoadTest().FirstOrDefault();

            if(pieceGame != null) 
            {
                pieceGame.PieceLocation = 14;
                pieceGame.GameId = Guid.NewGuid();
                pieceGame.PieceId = Guid.NewGuid();


                int rowsAffected = UpdateTest(pieceGame);
                Assert.IsTrue(rowsAffected == 1);
            }
            TestCleanup();
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
            TestCleanup();

        }
    }
}
