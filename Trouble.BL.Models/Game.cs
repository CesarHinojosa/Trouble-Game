using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.BL.Models
{
    public class Game
    {
        public Guid Id { get; set; }

        [DisplayName("Turn Number")]
        public int TurnNum { get; set; }

        [DisplayName("Game Name")]
        public string GameName { get; set; }

        [DisplayName("Game Date")]
        public DateTime GameDate { get; set; }

        [DisplayName("User Color")]
        public string UserColor {  get; set; }

    }
}
