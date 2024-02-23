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
    public class utUserGame : utBase<tblUserGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 4;
            var usergame = base.LoadTest();

            Assert.AreEqual(expected, usergame.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblUserGame usergame = new tblUserGame();   

            usergame.Id = Guid.NewGuid();
            usergame.GameId = Guid.NewGuid();
            usergame.UserId = Guid.NewGuid();

        }

        [TestMethod]
        public void UpdateTest() 
        {
            Guid newGuid = Guid.NewGuid();
            tblUserGame usergame = base.LoadTest().FirstOrDefault(x => x.GameId == newGuid);

            if(usergame != null)
            {
                
                usergame.GameId = Guid.NewGuid();
                usergame.UserId = Guid.NewGuid();

                int rowsAffected = UpdateTest(usergame);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblUserGame usergame = base.LoadTest().FirstOrDefault();

            if(usergame != null) 
            {
                int rowsAffected = DeleteTest(usergame);
                Assert.IsTrue(rowsAffected == 1);

            }
        }

    }
}
