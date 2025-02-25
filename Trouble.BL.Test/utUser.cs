﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.BL;

namespace Trouble.BL.Test
{
    [TestClass]
    public class utUser : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<User> users = new UserManager(options).Load();
            int expected = 6;

            Assert.AreEqual(expected, users.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User
            {
                Username = "Insert UserName BL",
                Password = GetHash("TestPassword"),
                FirstName = "Insert FirstName BL",
                LastName = "Insert LastName BL"
            };
             int result = new UserManager(options).Insert(user, true);
            Assert.IsTrue(result > 0);

            
        }

        [TestMethod]
        public void UpdateTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault();

            user.FirstName = "Update FirstName BL";

            Assert.IsTrue(new UserManager(options).Update(user, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault(r => r.Username == "User5");

            Assert.IsTrue(new UserManager(options).Delete(user.Id, true) > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault();

            Assert.AreEqual(new UserManager(options).LoadById(user.Id).Id, user.Id);
        }




        public static string GetHash(string Password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }


    }
}
