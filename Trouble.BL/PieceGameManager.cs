using Azure.Core;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Trouble.BL.Models;

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
            int greenStart = 1;
            int yellowStart = 8;
            int blueStart = 15;
            int redStart = 22;
            int greenEnd = 28;
            int yellowEnd = 7;
            int blueEnd = 14;
            int redEnd = 21;

            try
            {
                int results;
                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPieceGame row = dc.tblPieceGames.FirstOrDefault(r => r.GameId == gameId && r.PieceId == pieceId);
                    List<PieceGame> pieceGames = Load(gameId);
                    PieceGame pieceGame = pieceGames.FirstOrDefault(pg => pg.PieceId == pieceId);


                    //If piece is at home and roll is 1 or 6
                    if (row.PieceLocation == 0 && (spaces == 1 || spaces == 6))
                    {
                        if (pieceGame.PieceColor == "Green") row.PieceLocation = greenStart;
                        else if (pieceGame.PieceColor == "Yellow") row.PieceLocation = yellowStart;
                        else if (pieceGame.PieceColor == "Blue") row.PieceLocation = blueStart;
                        else row.PieceLocation = redStart;

                        //If piece is on home space, send that piece back to home
                        PieceGame pieceGame2 = pieceGames.FirstOrDefault(r => r.PieceLocation == row.PieceLocation && r.GameId == gameId);
                        if (pieceGame2 != null && pieceGame2.PieceLocation < 29 && pieceGame2.PieceColor != pieceGame.PieceColor)
                        {
                            tblPieceGame row2 = dc.tblPieceGames.FirstOrDefault(r => r.PieceLocation == row.PieceLocation && r.GameId == gameId);
                            row2.PieceLocation = 0;
                        }
                        //If piece of same color is at home, Don't move it
                        else if (pieceGame2 != null && pieceGame2.PieceColor == pieceGame.PieceColor)
                        {
                            row.PieceLocation = 0;
                        }
                    }
                    else if (row.PieceLocation > 0)
                    {
                        //Move piece forward
                        int previousLocation = row.PieceLocation;
                        row.PieceLocation += spaces;

                        if(row.PieceLocation > greenEnd)
                        {
                            if(previousLocation <= greenEnd)
                            {
                                if(pieceGame.PieceColor != "Green")
                                {
                                    row.PieceLocation = row.PieceLocation - 28;
                                }
                                else
                                {
                                    if(row.PieceLocation > 32) row.PieceLocation = previousLocation;
                                }
                            }

                            else
                            {
                                if (row.PieceLocation > 32) row.PieceLocation = previousLocation;
                            }
                        }

                        if (row.PieceLocation > yellowEnd && pieceGame.PieceColor == "Yellow")
                        {
                            if (previousLocation <= yellowEnd)
                            {
                                row.PieceLocation = (28 + spaces) - (yellowEnd - previousLocation);
                            }

                            if (row.PieceLocation > 32)
                            {
                                row.PieceLocation = previousLocation;
                            }
                        }

                        else if (row.PieceLocation > blueEnd && pieceGame.PieceColor == "Blue")
                        {
                            if (previousLocation <= blueEnd)
                            {
                                row.PieceLocation = 28 + spaces - (blueEnd - previousLocation);
                            }

                            if (row.PieceLocation > 32)
                            {
                                row.PieceLocation = previousLocation;
                            }
                        }

                        else if (row.PieceLocation > redEnd && pieceGame.PieceColor == "Red")
                        {
                            if (previousLocation <= redEnd)
                            {
                                row.PieceLocation = 28 + spaces - (redEnd - previousLocation);
                            }

                            if (row.PieceLocation > 32)
                            {
                                row.PieceLocation = previousLocation;
                            }
                        }


                        //If piece is on space, send that piece back to home
                        List<PieceGame> pieceGames2 = pieceGames.Where(r => r.PieceLocation == row.PieceLocation && r.GameId == gameId).ToList();
                        foreach(PieceGame pieceGame2 in pieceGames2) {
                            if (pieceGame2 != null && pieceGame2.PieceLocation < 29 && pieceGame2.PieceColor != pieceGame.PieceColor)
                            {
                                tblPieceGame row2 = dc.tblPieceGames.FirstOrDefault(r => r.PieceLocation == row.PieceLocation && r.GameId == gameId);
                                row2.PieceLocation = 0;
                            }
                            else if (pieceGame2 != null && pieceGame2.PieceColor == pieceGame.PieceColor)
                            {
                                row.PieceLocation = previousLocation;
                            }
                        }
                    }

                    if(spaces != 6 && (row.PieceLocation != pieceGame.PieceLocation || (row.PieceLocation == 0 && pieceGame.PieceLocation == 0)))
                    {
                        GameManager gm = new GameManager(options);
                        Game game = gm.LoadById(gameId);
                        game.TurnNum++;
                        if (game.TurnNum > 3) game.TurnNum = 0;
                        gm.Update(game);

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

        public PieceGame ComputerMovePiece(Guid gameId, string color, int spaces)
        {
            try
            {
                int greenEnd = 28;
                int yellowEnd = 7;
                int blueEnd = 14;
                int redEnd = 21;

                List<PieceGame> pieceGames = Load(gameId);
                PieceGame pieceToMove = null;

                List<PieceGame> piecesToMove = pieceGames.Where(p => p.PieceColor == color).ToList();

                foreach (PieceGame pieceGame in piecesToMove)
                {
                    int newLocation = pieceGame.PieceLocation + spaces;
                    bool pieceCanMove = true;
                    if (pieceGame.PieceLocation + spaces > greenEnd)
                    {
                        if (pieceGame.PieceLocation <= greenEnd)
                        {
                            if (pieceGame.PieceColor != "Green")
                            {
                                newLocation = newLocation - 28;
                                pieceCanMove = true;
                            }
                        }

                        else
                        {
                            if (newLocation > 32) pieceCanMove = false;
                        }
                    }

                    if (newLocation > yellowEnd && color == "Yellow")
                    {
                        if (pieceGame.PieceLocation <= yellowEnd)
                        {
                            newLocation = (28 + spaces) - (yellowEnd - pieceGame.PieceLocation);
                            pieceCanMove = true;
                        }

                        if (newLocation > 32)
                        {
                            pieceCanMove = false;
                        }
                    }

                    else if (newLocation > blueEnd && color == "Blue")
                    {
                        if (pieceGame.PieceLocation <= blueEnd)
                        {
                            newLocation = (28 + spaces) - (blueEnd - pieceGame.PieceLocation);
                            pieceCanMove = true;
                        }

                        if (newLocation > 32)
                        {
                            pieceCanMove = false;
                        }
                    }

                    else if (newLocation > redEnd && color == "Red")
                    {
                        if (pieceGame.PieceLocation <= redEnd)
                        {
                            newLocation = (28 + spaces) - (redEnd - pieceGame.PieceLocation);
                            pieceCanMove = true;
                        }

                        if (newLocation > 32)
                        {
                            pieceCanMove = false;
                        }
                    }
                    
                    if (pieceCanMove) pieceCanMove = !pieceGames.Any(p => p.PieceLocation == newLocation && p.PieceColor == color);
                    if (pieceCanMove && pieceToMove == null) pieceToMove = pieceGame;
                    else if (pieceCanMove && pieceGame.PieceLocation > pieceToMove.PieceLocation) pieceToMove = pieceGame;

                }

                if (pieceToMove == null) return null;
                if (pieceToMove.PieceLocation == 0 && (spaces != 1 && spaces != 6)) return null;

                return pieceToMove;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
