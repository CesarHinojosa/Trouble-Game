using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.BL
{
    public class UserGameManager : GenericManager<tblUserGame>
    {
        public UserGameManager(DbContextOptions<TroubleEntities> options) : base(options)
        {

        }

        public List<UserGame> Load()
        {
            try
            {
                List<UserGame> rows = new List<UserGame>();
                base.Load()
                    .ForEach(d => rows.Add(
                        new UserGame
                        {
                            Id = d.Id,
                            UserId = d.UserId,
                            GameId = d.GameId

                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserGame LoadById(Guid id)
        {
            try
            {
                tblUserGame row = base.LoadById(id);

                if (row != null)
                {
                    UserGame userGame = new UserGame
                    {
                        Id = row.Id,
                        UserId = row.UserId,
                        GameId = row.GameId
                    };
                    return userGame;
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

        public int Insert(Guid userId, Guid gameId, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblUserGame row = new tblUserGame();

                    row.Id = Guid.NewGuid();
                    row.GameId = gameId;
                    row.UserId = userId;

                    dc.tblUserGames.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(Guid userId, Guid gameId, bool rollback = false)
        {
            try
            {
                int results;
                using (TroubleEntities dc = new TroubleEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserGame row = dc.tblUserGames.FirstOrDefault(r => r.UserId == userId && r.GameId == gameId);

                    if(row != null)
                    {
                        row.UserId = userId;
                        row.GameId = gameId;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                    return results;
                }
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
