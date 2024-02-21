using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.PL.Data
{
    public class tblPieceGame
    {
        public Guid Id { get; set; }

        public Guid PieceId { get; set; }

        public Guid GameId { get; set; }

        public string PieceLocation { get; set; }
    }
}
