using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.BL.Models
{
    public class PieceGame
    {
        public Guid Id {  get; set; }    
        public Guid PieceId { get; set; }
        public Guid GameId { get; set; }
        public int PieceLocation {  get; set; }
    }
}
