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
    public class utUser : utBase<tblUser>
    {

        [TestMethod]
        public void LoadTest()
        {
            int expected = 5;
            var user = base.LoadTest();
            Assert.AreEqual(expected, user.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblUser newRow = new tblUser();

            newRow.Id = Guid.NewGuid();
            newRow.FirstName = "Test1";
            newRow.LastName = "Test2";
            newRow.Username = "Test2UserName";
            newRow.Password = GetHash("TestPassword");

            int results = InsertTest(newRow);
            Assert.AreEqual(1, results);

        }

        [TestMethod]
        public void UpdateTest()
        {
            tblUser user = base.LoadTest().FirstOrDefault();
            if (user != null)
            {
                user.FirstName = "Updated First Name";
                user.LastName = "Updated Last Name";
                int rowsAffected = UpdateTest(user);
                Assert.IsTrue(rowsAffected == 1);

            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUser user = base.LoadTest().FirstOrDefault(x => x.FirstName == "Test");
            if (user != null)
            {
                
                int rowsAffected = DeleteTest(user);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

        
    }
}
