using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.PL.Data
{
    public class tblPlayerGame
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public Guid GameId { get; set; }
    }
}
