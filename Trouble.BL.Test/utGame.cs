using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.Bl.Test;

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
    }
}
