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
    public class utUser : utBase<tblUser>
    {

        [TestMethod]
        public void LoadTest()
        {
            int expected = 4;
            var customers = base.LoadTest();
            Assert.AreEqual(expected, customers.Count());
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
            tblUser user = base.LoadTest().FirstOrDefault();
            if (user != null)
            {
                
                int rowsAffected = DeleteTest(user);
                Assert.IsTrue(rowsAffected == 1);
            }

        }

        
    }
}
