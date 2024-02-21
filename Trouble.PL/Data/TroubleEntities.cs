using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouble.PL.Entities;

namespace Trouble.PL.Data
{
    public class TroubleEntities : DbContext
    {
        Guid[] userId = new Guid[4];
        Guid[] gameId = new Guid[2];
        Guid[] pieceId = new Guid[16];

        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblGame> tblGames { get; set; }
        public virtual DbSet<tblPiece> tblPieces { get; set; }
        public virtual DbSet<tblUserGame> tblUserGames { get; set; }
        public virtual DbSet<tblPieceGame> tblPieceGames { get; set; }

        public TroubleEntities(DbContextOptions<TroubleEntities> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public TroubleEntities() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            CreateGames(modelBuilder);
            CreatePieces(modelBuilder);
            CreateUsers(modelBuilder);
            CreatePieceGame(modelBuilder);
            CreateUserGame(modelBuilder);
        }

        private void CreateUsers(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < userId.Length; i++)
            {
                userId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUser_Id");

                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(28)
                .IsUnicode(false);

                entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[0],
                FirstName = "Bob",
                LastName = "Bob",
                Username = "User1",
                Password = GetHash("Test")
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[1],
                FirstName = "Joe",
                LastName = "Joe",
                Username = "User2",
                Password = GetHash("Test")
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[2],
                FirstName = "Susan",
                LastName = "Susan",
                Username = "User3",
                Password = GetHash("Test")
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[3],
                FirstName = "Sally",
                LastName = "Sally",
                Username = "User4",
                Password = GetHash("Test")
            });

        }

        private void CreatePieces(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < pieceId.Length; i++)
            {
                pieceId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblPiece>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblPiece_Id");

                entity.ToTable("tblPiece");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            List<tblPiece> Pieces = new List<tblPiece>
            {
                new tblPiece {Id = pieceId[0], Color = "Red"},
                new tblPiece {Id = pieceId[1], Color = "Red"},
                new tblPiece {Id = pieceId[2], Color = "Red"},
                new tblPiece {Id = pieceId[3], Color = "Red"},
                new tblPiece {Id = pieceId[4], Color = "Blue"},
                new tblPiece {Id = pieceId[5], Color = "Blue"},
                new tblPiece {Id = pieceId[6], Color = "Blue"},
                new tblPiece {Id = pieceId[7], Color = "Blue"},
                new tblPiece {Id = pieceId[8], Color = "Green"},
                new tblPiece {Id = pieceId[9], Color = "Green"},
                new tblPiece {Id = pieceId[10], Color = "Green"},
                new tblPiece {Id = pieceId[11], Color = "Green"},
                new tblPiece {Id = pieceId[12], Color = "Yellow"},
                new tblPiece {Id = pieceId[13], Color = "Yellow"},
                new tblPiece {Id = pieceId[14], Color = "Yellow"},
                new tblPiece {Id = pieceId[15], Color = "Yellow"},
            };
            modelBuilder.Entity<tblPiece>().HasData(Pieces);

        }
    
        private void CreateGames(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < gameId.Length; i++)
            {
                gameId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblGame_Id");
                entity.ToTable("tblGame");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.GameDate).HasColumnType("datetime");
                entity.Property(e => e.GameName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                List<tblGame> Games = new List<tblGame>
                {
                    new tblGame
                    {
                        Id = gameId[0],
                        GameName = "Game1",
                        GameDate = DateTime.Now,
                        TurnNum = 0
                    },

                    new tblGame
                    {
                        Id = gameId[1],
                        GameName = "Game2",
                        GameDate = DateTime.Now,
                        TurnNum = 0
                    }
                };
                modelBuilder.Entity<tblGame>().HasData(Games);
            });
        }

        private void CreateUserGame(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUserGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUserGame_Id");

                entity.ToTable("tblUserGame");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(u => u.User)
                .WithMany(p => p.tblUserGames)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tblUserGame_UserId");

                entity.HasOne(u => u.Game)
                .WithMany(p => p.tblUserGames)
                .HasForeignKey(u => u.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tblUserGame_GameId");
            });

            List<tblUserGame> UserGames = new List<tblUserGame>
            {
                new tblUserGame {Id = Guid.NewGuid(), UserId = userId[0], GameId = gameId[0]},
                new tblUserGame {Id = Guid.NewGuid(), UserId = userId[1], GameId = gameId[0]},
                new tblUserGame {Id = Guid.NewGuid(), UserId = userId[2], GameId = gameId[0]},
                new tblUserGame {Id = Guid.NewGuid(), UserId = userId[3], GameId = gameId[0]},
            };
            modelBuilder.Entity<tblUserGame>().HasData(UserGames);
        }

        private void CreatePieceGame(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblPieceGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblPieceGame_Id");

                entity.ToTable("tblPieceGame");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(p => p.Piece)
                .WithMany(g => g.tblPieceGames)
                .HasForeignKey(p => p.PieceId)
                .HasConstraintName("tblPieceGame_PieceId");

                entity.HasOne(g => g.Game)
                .WithMany(p => p.tblPieceGames)
                .HasForeignKey(g => g.GameId)
                .HasConstraintName("tblPieceGame_GameId");
            });

            List<tblPieceGame> PieceGames = new List<tblPieceGame>
            {
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[0], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[1], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[2], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[3], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[4], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[5], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[6], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[7], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[8], PieceLocation = 0},
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[9], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[10], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[11], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[12], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[13], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[14], PieceLocation = 0 },
                new tblPieceGame { Id = Guid.NewGuid(), GameId = gameId[0], PieceId = pieceId[15], PieceLocation = 0 },
            };

            modelBuilder.Entity<tblPieceGame>().HasData(PieceGames);
        }


        private static string GetHash(string Password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }
    }
}
