﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trouble.PL.Entities
{
    public class tblUserGame : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public string PlayerColor { get; set; }

        public virtual tblUser User { get; set; }
        public virtual tblGame Game { get; set; }
    }
}
