using Azure.Core;
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

        public List<PieceGame> Load(Guid? gameId = null)
        {
            try
            {
                List<PieceGame> rows = new List<PieceGame>();

                using (TroubleEntities dc = new TroubleEntities(options)) 
                {
                    rows = (from pg in dc.tblPieceGames
                            join p in dc.tblPieces on pg.PieceId equals p.Id
                            where pg.GameId == gameId|| gameId == null
                            select new PieceGame
                            {
                                Id = pg.Id,
                                PieceId = pg.PieceId,
                                GameId = pg.GameId,
                                PieceLocation = pg.PieceLocation,
                                PieceColor = p.Color

                            })
                            .Distinct()
                            .ToList();
                }

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PieceGame LoadById(Guid id)
        {
            try
            {
                tblPieceGame row = base.LoadById(id);

                if (row != null)
                {
                    PieceGame pieceGame = new PieceGame
                    {
                        Id = row.Id,
                        PieceId = row.PieceId,
                        GameId = row.GameId,
                        PieceLocation = row.PieceLocation
                    };
                    return pieceGame;
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

        public int Update(Guid pieceId, Guid gameId, int location, bool rollback = false)
        {
            try
            {
                int results;
                using (TroubleEntities dc = new TroubleEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPieceGame row = dc.tblPieceGames.FirstOrDefault(r => r.GameId == gameId && r.PieceId == pieceId);

                    if (row != null)
                    {
                        row.PieceId = pieceId;
                        row.GameId = gameId;
                        row.PieceLocation = location;

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

        public int MovePiece(Guid pieceId, Guid gameId, int spaces, bool rollback = false)
        {
            try
            {
                int results;
                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPieceGame row = dc.tblPieceGames.FirstOrDefault(r => r.GameId == gameId && r.PieceId == pieceId);

                    //If piece is at home and roll is 1 or 6
                    if (row.PieceLocation == 0 && (spaces == 1 || spaces == 6))
                    {
                        row.PieceLocation = 1;
                    }
                    else
                    {
                        //If piece is on space, send that piece back to home
                        tblPieceGame row2 = dc.tblPieceGames.FirstOrDefault(r => r.PieceLocation == row.PieceLocation + spaces && r.GameId == gameId);
                        if(row2 != null)
                        {
                            row2.PieceLocation = 0;
                        }

                        //Move piece forward
                        row.PieceLocation += spaces;
                    }
                    dc.SaveChanges();
                    if (rollback) transaction.Rollback();
                    return row.PieceLocation;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
