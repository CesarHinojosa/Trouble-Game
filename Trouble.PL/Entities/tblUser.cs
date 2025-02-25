﻿namespace Trouble.PL.Entities
{
    public class tblUser : IEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<tblUserGame> tblUserGames { get; set; }
    }

    
}
