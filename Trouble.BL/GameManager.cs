using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Trouble.BL.Models;
using Trouble.PL.Entities;

namespace Trouble.BL
{
    public class GameManager : GenericManager<tblGame>
    {
        public GameManager(DbContextOptions<TroubleEntities> options) : base(options)
        {

        }

        public List<Game> Load()
        {
            try
            {
                List<Game> rows = new List<Game>();
                base.Load()
                    .ForEach(d => rows.Add(
                        new Game
                        {
                            Id = d.Id,
                            TurnNum = d.TurnNum,
                            GameName = d.GameName,
                            GameDate = d.GameDate
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Game LoadById(Guid id)
        {
            try
            {
                tblGame row = base.LoadById(id);

                if (row != null)
                {
                    Game game = new Game
                    {
                        Id = row.Id,
                        TurnNum = row.TurnNum,
                        GameName = row.GameName,
                        GameDate = row.GameDate
                    };
                    return game;
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

        public List<Game> LoadByUserId(Guid userId)
        {
            try
            {
                List<Game> rows = new List<Game>();

                using (TroubleEntities dc = new TroubleEntities(options))
                {
                    rows = (from g in dc.tblGames
                            join p in dc.tblUserGames on g.Id equals p.GameId
                            where p.UserId == userId || userId == null
                            select new Game
                            {
                                Id = g.Id,
                                GameName = g.GameName,
                                GameDate = g.GameDate,
                                TurnNum = g.TurnNum,
                                UserColor = p.PlayerColor
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

        public int Insert(Game game, bool rollback = false)
        {
            try
            {
                tblGame entity = new tblGame();
                entity.Id = new Guid();
                entity.TurnNum = game.TurnNum;
                entity.GameName = game.GameName;
                entity.GameDate = DateTime.Now;


                int result = base.Insert(entity, rollback);
                game.Id = entity.Id;

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(Game game, bool rollback = false)
        {
            try
            {
                int results = base.Update(new tblGame
                {
                    Id = game.Id,
                    GameName = game.GameName,
                    GameDate = game.GameDate,
                    TurnNum = game.TurnNum
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


        public int Roll()
        {
            Random rnd = new Random();
            int roll = rnd.Next(1, 7);
            return roll;
        }
    }
}
