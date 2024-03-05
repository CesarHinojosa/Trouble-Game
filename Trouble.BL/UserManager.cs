

using System.Runtime;
using Trouble.BL.Models;

namespace Trouble.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot log in with these credentials.")
        {
        }

        public LoginFailureException(string message) : base(message)
        {
        }
    }

    public class UserManager : GenericManager<tblUser>
    {
        public UserManager(DbContextOptions<TroubleEntities> options) : base(options)
        {

        }

        public string GetHash(string password)
        {
            using (var hasher = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public void Seed()
        {
            List<User> users = Load();

            foreach (User user in users)
            {
                if(user.Password.Length != 28)
                {
                    Update(user);
                }
            }

            if(users.Count == 0)
            {
                Insert(new User { Username = "test", Password="test", FirstName = "test", LastName = "test" });
                Insert(new User { Username = "test2", Password = "test", FirstName = "test2", LastName = "test2" });
            }
        }

        public bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Username))
                {
                    if(!string.IsNullOrEmpty(user.Password))
                    {
                        using (TroubleEntities dc = new TroubleEntities(options))
                        {
                            tblUser userrow = dc.tblUsers.FirstOrDefault(u => u.Username == user.Username);

                            if (userrow != null)
                            {
                                if (userrow.Password == GetHash(user.Password))
                                {
                                    user.Id = userrow.Id;
                                    user.Username = userrow.Username;
                                    user.Password = userrow.Password;
                                    user.FirstName = userrow.FirstName;
                                    user.LastName = userrow.LastName;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException("Cannot log in with these credentials");
                                }
                            }
                            else
                            {
                                throw new Exception("User was not found");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Password not set");
                    }
                }
                else
                {
                    throw new Exception("Username not set");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<User> Load()
        {
            try
            {
                List<User> rows = new List<User>();
                base.Load()
                    .ForEach(d => rows.Add(
                        new User
                        {
                            Id = d.Id,
                            Username = d.Username,
                            Password = d.Password,
                            FirstName = d.FirstName,
                            LastName = d.LastName
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User LoadById(Guid id)
        {
            try
            {
                tblUser row = base.LoadById(id);

                if(row != null)
                {
                    User user = new User
                    {
                        Id = row.Id,
                        Username = row.Username,
                        Password = row.Password,
                        FirstName = row.FirstName,
                        LastName = row.LastName
                    };
                    return user;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Insert(User user, bool rollback = false)
        {
            try
            {
                tblUser entity = new tblUser();
                entity.Id = new Guid();
                entity.Username = user.Username;
                entity.Password = user.Password;
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                
                user.Id = entity.Id;

                return base.Insert(entity, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(User user, bool rollback = false)
        {
            try
            {
                int results = base.Update(new tblUser
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }, rollback);

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return base.Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
