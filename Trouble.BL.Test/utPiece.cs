using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.Bl.Test;

namespace Trouble.BL.Test
{
    [TestClass]
    public class utPiece : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Piece> pieces = new PieceManager(options).Load();
            int expected = 16;
            Assert.AreEqual(expected, pieces.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Piece piece = new Piece
            {
                Color = "StrawBerry"
            };
            int result = new PieceManager(options).Insert(piece, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Piece piece = new PieceManager(options).Load().FirstOrDefault();
            piece.Color = "Updated Color BL";
            Assert.IsTrue(new PieceManager(options).Update(piece, true) > 0);
        }

        [TestMethod]
        public void DeleteTest() 
        {
            Piece piece = new PieceManager(options).Load().FirstOrDefault();
            
            Assert.IsTrue(new PieceManager(options).Delete(piece.Id, true) > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Piece piece = new PieceManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new PieceManager(options).LoadById(piece.Id).Id, piece.Id); 
        }
    }
}
