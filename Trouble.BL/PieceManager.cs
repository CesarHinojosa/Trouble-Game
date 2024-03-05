using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.BL
{
    public class PieceManager : GenericManager<tblPiece>
    {
        public PieceManager(DbContextOptions<TroubleEntities> options) : base(options)
        {

        }

        public List<Piece> Load()
        {
            try
            {
                List<Piece> rows = new List<Piece>();
                base.Load()
                    .ForEach(d => rows.Add(
                        new Piece
                        {
                            Id = d.Id,
                            Color = d.Color
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Piece LoadById(Guid id)
        {
            try
            {
                tblPiece row = base.LoadById(id);

                if (row != null)
                {
                    Piece piece = new Piece
                    {
                        Id = row.Id,
                        Color = row.Color
                    };
                    return piece;
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

        public int Insert(Piece piece, bool rollback = false)
        {
            try
            {
                tblPiece entity = new tblPiece();
                entity.Id = new Guid();
                entity.Color = piece.Color;

                piece.Id = entity.Id;

                return base.Insert(entity, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(Piece piece, bool rollback = false)
        {
            try
            {
                int results = base.Update(new tblPiece
                {
                    Id = piece.Id,
                    Color = piece.Color
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
