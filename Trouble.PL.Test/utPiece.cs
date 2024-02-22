using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.PL.Entities;
using Trouble.PL.Test;

namespace Trouble.PL.Test
{
    [TestClass]
    public class utPiece : utBase<tblPiece>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 16;
            var piece = base.LoadTest();
            Assert.AreEqual(expected, piece.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPiece piece = new tblPiece();

            piece.Id = Guid.NewGuid();
            piece.Color = "Purple";
            TestCleanup();

        }

        [TestMethod]

        public void UpdateTest()
        {
            tblPiece piece = base.LoadTest().FirstOrDefault();

            if(piece != null) 
            {
                piece.Color = "CornFlower Blue";
                int rowsAffected = UpdateTest(piece);

                Assert.IsTrue(rowsAffected == 1);
                TestCleanup();
            }
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblPiece piece = base.LoadTest().FirstOrDefault();
            if(piece != null) 
            {
                int rowsAffected = DeleteTest(piece);
                Assert.IsTrue(rowsAffected == 1);
                TestCleanup();
            }
        }
    }
}
