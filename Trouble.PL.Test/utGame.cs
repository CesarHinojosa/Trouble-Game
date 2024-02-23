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
    public class utGame : utBase<tblGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 2;
            var game = base.LoadTest();
            Assert.AreEqual(expected, game.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblGame game = new tblGame();

            game.Id = Guid.NewGuid();
            game.TurnNum = 5;
            game.GameName = "Inserted Game";
            game.GameDate = DateTime.Now;

            int results = base.InsertTest(game);
            Assert.AreEqual(1, results);

        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGame game = base.LoadTest().FirstOrDefault();

            if(game != null)
            {
                game.GameName = "Updated Game Name";
                game.GameDate = DateTime.Now.AddDays(5);

                int rowsAffected = UpdateTest(game);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
        [TestMethod]
        public void DeleteTest() 
        {
            tblGame game = base.LoadTest().FirstOrDefault();

            if(game != null ) 
            {
                int rowsAffected = DeleteTest(game);
                Assert.IsTrue(rowsAffected == 1);
                
            }
        }
    }
}
