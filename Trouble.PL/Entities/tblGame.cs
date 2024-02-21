using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.PL.Entities
{
    public class tblGame
    {
        public Guid Id { get; set; }

        public int TurnNum { get; set; }

        public string GameName { get; set; }

        public DateTime GameDate { get; set; }

        public virtual ICollection<tblUserGame> tblUserGames { get; set; }
        public virtual ICollection<tblPieceGame> tblPieceGames { get; set; }
    }
}
