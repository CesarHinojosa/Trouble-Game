using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Insert(Game game, bool rollback = false)
        {
            try
            {
                tblGame entity = new tblGame();
                entity.Id = new Guid();
                entity.TurnNum = game.TurnNum;
                entity.GameName = game.GameName;
                entity.GameDate = game.GameDate;

                game.Id = entity.Id;

                return base.Insert(entity, rollback);
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
    }
}
