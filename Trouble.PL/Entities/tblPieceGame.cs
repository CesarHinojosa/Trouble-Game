using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.PL.Entities
{
    public class tblPieceGame : IEntity
    {
        public Guid Id { get; set; }

        public Guid PieceId { get; set; }

        public Guid GameId { get; set; }

        public int PieceLocation { get; set; }

        public virtual tblPiece Piece { get; set; }
        public virtual tblGame Game { get; set; }
    }
}
