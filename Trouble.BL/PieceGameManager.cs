using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.BL
{
    public class PieceGameManager : GenericManager<tblPieceGame>
    {
        public PieceGameManager(DbContextOptions<TroubleEntities> options) : base(options)
        {

        }

        public int Insert(Guid pieceId, Guid gameId, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblPieceGame row = new tblPieceGame();

                    row.Id = Guid.NewGuid();
                    row.GameId = gameId;
                    row.PieceId = pieceId;

                    dc.tblPieceGames.Add(row);

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

        public int Update(Guid pieceId, Guid gameId, bool rollback = false)
        {
            try
            {
                int results;
                using (TroubleEntities dc = new TroubleEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPieceGame row = dc.tblPieceGames.FirstOrDefault(r => r.PieceId == pieceId && r.GameId == gameId);

                    if (row != null)
                    {
                        row.PieceId = pieceId;
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
