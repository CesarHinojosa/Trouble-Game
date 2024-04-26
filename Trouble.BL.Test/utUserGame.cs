using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.BL.Test;

namespace Trouble.BL.Test
{
    [TestClass]
    public class UserGame : utBase
    {

        [TestMethod]
        public void InsertTest()
        {


            Game game = new GameManager(options).Load().FirstOrDefault();
            //Assert.AreEqual(new UserGameManager(options).LoadById(game.Id).Id, game.Id);

            User user = new UserManager(options).Load().FirstOrDefault();
            //Assert.AreEqual(new UserGameManager(options).LoadById(user.Id).Id, user.Id);

            int result = new UserGameManager(options).Insert(user.Id, game.Id, "Green", true);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            Guid row = new UserGameManager(options).Load().FirstOrDefault().Id;
            Assert.IsTrue(new UserGameManager(options).Delete(row, true) > 0);
        }



    }
}
