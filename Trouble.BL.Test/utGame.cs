using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.BL.Test;
using Trouble.BL.Models;

namespace Trouble.BL.Test
{
    [TestClass]
    public class utGame : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Game> game = new GameManager(options).Load();
            int expected = 2;

            Assert.AreEqual(expected, game.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Game game = new Game
            {
                GameDate = DateTime.Now,
                TurnNum = 88,
                GameName = "Insert Game Name BL"
            };

            int result = new GameManager(options).Insert(game, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault();
            game.TurnNum = 99;
            game.GameName = "Updated Game Name BL";

            Assert.IsTrue(new GameManager(options).Update(game, true) > 0);
        }

        [TestMethod]
        public void DeleteTest() 
        {
            Game game = new GameManager(options).Load().FirstOrDefault(x => x.GameName == "Game2");

            Assert.IsTrue(new GameManager(options).Delete(game.Id, true) > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new GameManager(options).LoadById(game.Id).Id, game.Id);
        }

        [TestMethod]
        public void RollTest()
        {
            int randomNum = new GameManager(options).Roll();
            Assert.IsTrue(randomNum <= 6 && randomNum >= 1);
        }
    }
}
