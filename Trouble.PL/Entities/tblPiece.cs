using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.PL.Entities
{
    public class tblPiece : IEntity
    {
        public Guid Id { get; set; }

        public string Color { get; set; }

        public virtual ICollection<tblPieceGame> tblPieceGames { get; set; }
    }
}
