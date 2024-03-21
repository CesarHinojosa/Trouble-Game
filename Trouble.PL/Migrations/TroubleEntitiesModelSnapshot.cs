﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trouble.PL.Data;

#nullable disable

namespace Trouble.PL.Migrations
{
    [DbContext(typeof(TroubleEntities))]
    partial class TroubleEntitiesModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Trouble.PL.Entities.tblGame", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TurnNum")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_tblGame_Id");

                    b.ToTable("tblGame", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            GameDate = new DateTime(2024, 3, 21, 17, 25, 50, 353, DateTimeKind.Local).AddTicks(7909),
                            GameName = "Game1",
                            TurnNum = 0
                        },
                        new
                        {
                            Id = new Guid("fac944a2-36cd-43ff-9c21-8c63a90e6160"),
                            GameDate = new DateTime(2024, 3, 21, 17, 25, 50, 353, DateTimeKind.Local).AddTicks(7957),
                            GameName = "Game2",
                            TurnNum = 0
                        });
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblPiece", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id")
                        .HasName("PK_tblPiece_Id");

                    b.ToTable("tblPiece", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5fe75ac6-a73e-432c-9c17-6095af2d4aef"),
                            Color = "Red"
                        },
                        new
                        {
                            Id = new Guid("1f3f10b6-9b51-4e53-b478-561a0f55fefc"),
                            Color = "Red"
                        },
                        new
                        {
                            Id = new Guid("b2430105-0289-45fc-95fc-7115c14393ed"),
                            Color = "Red"
                        },
                        new
                        {
                            Id = new Guid("904bc09b-d190-4745-a667-d2eac333bcfe"),
                            Color = "Red"
                        },
                        new
                        {
                            Id = new Guid("10279fe5-a3dd-4770-afb6-82a4f350cea5"),
                            Color = "Blue"
                        },
                        new
                        {
                            Id = new Guid("27fbed5f-b6de-4b93-9a4d-6eaae598d157"),
                            Color = "Blue"
                        },
                        new
                        {
                            Id = new Guid("d21b8cd5-ec32-4893-96e4-199312480fa6"),
                            Color = "Blue"
                        },
                        new
                        {
                            Id = new Guid("9c041be6-0049-4060-8c3d-0a1711dff500"),
                            Color = "Blue"
                        },
                        new
                        {
                            Id = new Guid("04605c78-c0f0-4713-869d-96235dadea16"),
                            Color = "Green"
                        },
                        new
                        {
                            Id = new Guid("d556e30c-6d87-48c3-97c9-3ae6335a2cb0"),
                            Color = "Green"
                        },
                        new
                        {
                            Id = new Guid("0e041e67-51c3-4d6e-a7b8-3684c5a9a793"),
                            Color = "Green"
                        },
                        new
                        {
                            Id = new Guid("33231a13-5152-4ead-b34f-7c903d4bac54"),
                            Color = "Green"
                        },
                        new
                        {
                            Id = new Guid("f479f108-fffb-472a-80b2-3d4b00b7b4d0"),
                            Color = "Yellow"
                        },
                        new
                        {
                            Id = new Guid("42b17ef7-36cf-4a45-b1b7-5595796565cc"),
                            Color = "Yellow"
                        },
                        new
                        {
                            Id = new Guid("b800a56e-863d-4cfc-997b-5ecad5c7c92b"),
                            Color = "Yellow"
                        },
                        new
                        {
                            Id = new Guid("6c2adcf3-7546-459d-a231-bcd0796624bb"),
                            Color = "Yellow"
                        });
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblPieceGame", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PieceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PieceLocation")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_tblPieceGame_Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PieceId");

                    b.ToTable("tblPieceGame", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("878e9d11-48be-4759-8762-e6a71a9eee0d"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("5fe75ac6-a73e-432c-9c17-6095af2d4aef"),
                            PieceLocation = 5
                        },
                        new
                        {
                            Id = new Guid("0cc3cca0-0921-4db7-adea-fe9239a93f24"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("1f3f10b6-9b51-4e53-b478-561a0f55fefc"),
                            PieceLocation = 2
                        },
                        new
                        {
                            Id = new Guid("c0b6c1fd-15df-4868-8072-ef54d0a925ca"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("b2430105-0289-45fc-95fc-7115c14393ed"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("588a0cf6-5ca6-4d54-8aec-1d49ed748e2d"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("904bc09b-d190-4745-a667-d2eac333bcfe"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("c4414ced-ef84-44e6-a222-dd3114041a1d"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("10279fe5-a3dd-4770-afb6-82a4f350cea5"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("d12ff279-0ccb-49f2-a398-96ad6702a1f5"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("27fbed5f-b6de-4b93-9a4d-6eaae598d157"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("19ad4e4c-066e-4c80-b826-c9508e94c295"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("d21b8cd5-ec32-4893-96e4-199312480fa6"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("b4b08fb2-b436-4b3a-8fdb-b67b61926a3b"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("9c041be6-0049-4060-8c3d-0a1711dff500"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("9d6c13e4-9c37-4462-b155-34fc40e1727d"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("04605c78-c0f0-4713-869d-96235dadea16"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("986f4b4e-63c7-4c8d-b301-524463eade8e"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("d556e30c-6d87-48c3-97c9-3ae6335a2cb0"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("ef0a462a-6858-4c47-920c-0ea86b81c542"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("0e041e67-51c3-4d6e-a7b8-3684c5a9a793"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("e2e08cef-f9bc-43a0-b178-3634a3cf0548"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("33231a13-5152-4ead-b34f-7c903d4bac54"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("1405a4e3-2a27-4c95-b2e7-b6c308f122b3"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("f479f108-fffb-472a-80b2-3d4b00b7b4d0"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("a95d0af4-6a9b-4504-8187-ee68b222517d"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("42b17ef7-36cf-4a45-b1b7-5595796565cc"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("fdfb81d8-03c8-4d28-9df0-432a11981383"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("b800a56e-863d-4cfc-997b-5ecad5c7c92b"),
                            PieceLocation = 0
                        },
                        new
                        {
                            Id = new Guid("452219fe-a33f-4324-ba70-68fc7c07f948"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            PieceId = new Guid("6c2adcf3-7546-459d-a231-bcd0796624bb"),
                            PieceLocation = 0
                        });
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(28)
                        .IsUnicode(false)
                        .HasColumnType("varchar(28)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id")
                        .HasName("PK_tblUser_Id");

                    b.ToTable("tblUser", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("83063991-18b4-431b-9944-39e1d41c104d"),
                            FirstName = "Bob",
                            LastName = "Bob",
                            Password = "ZAqyuuB77cTBY/Z5p0b3q3+10fo=",
                            Username = "User1"
                        },
                        new
                        {
                            Id = new Guid("7b5db418-8974-4dae-9d39-c44aa4bc08c3"),
                            FirstName = "Joe",
                            LastName = "Joe",
                            Password = "ZAqyuuB77cTBY/Z5p0b3q3+10fo=",
                            Username = "User2"
                        },
                        new
                        {
                            Id = new Guid("4aef32ba-570e-449d-b1fc-14f18c2823aa"),
                            FirstName = "Susan",
                            LastName = "Susan",
                            Password = "ZAqyuuB77cTBY/Z5p0b3q3+10fo=",
                            Username = "User3"
                        },
                        new
                        {
                            Id = new Guid("886eef82-fe91-4483-a32c-8b10f0a569be"),
                            FirstName = "Sally",
                            LastName = "Sally",
                            Password = "ZAqyuuB77cTBY/Z5p0b3q3+10fo=",
                            Username = "User4"
                        },
                        new
                        {
                            Id = new Guid("29a8209c-bc1e-45fb-975a-b3be403c8eb7"),
                            FirstName = "Test",
                            LastName = "Test",
                            Password = "ZAqyuuB77cTBY/Z5p0b3q3+10fo=",
                            Username = "User5"
                        });
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblUserGame", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK_tblUserGame_Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("tblUserGame", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("80a923ca-3a79-4187-8b4c-c85188961d97"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            UserId = new Guid("83063991-18b4-431b-9944-39e1d41c104d")
                        },
                        new
                        {
                            Id = new Guid("a92b4470-4f9e-47be-b5f5-37aa25492db0"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            UserId = new Guid("7b5db418-8974-4dae-9d39-c44aa4bc08c3")
                        },
                        new
                        {
                            Id = new Guid("3849a90a-f2e0-485a-a0e2-bc239e6d3e66"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            UserId = new Guid("4aef32ba-570e-449d-b1fc-14f18c2823aa")
                        },
                        new
                        {
                            Id = new Guid("8f3434e3-8952-44ee-8864-ee697f345122"),
                            GameId = new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"),
                            UserId = new Guid("886eef82-fe91-4483-a32c-8b10f0a569be")
                        });
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblPieceGame", b =>
                {
                    b.HasOne("Trouble.PL.Entities.tblGame", "Game")
                        .WithMany("tblPieceGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tblPieceGame_GameId");

                    b.HasOne("Trouble.PL.Entities.tblPiece", "Piece")
                        .WithMany("tblPieceGames")
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tblPieceGame_PieceId");

                    b.Navigation("Game");

                    b.Navigation("Piece");
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblUserGame", b =>
                {
                    b.HasOne("Trouble.PL.Entities.tblGame", "Game")
                        .WithMany("tblUserGames")
                        .HasForeignKey("GameId")
                        .IsRequired()
                        .HasConstraintName("fk_tblUserGame_GameId");

                    b.HasOne("Trouble.PL.Entities.tblUser", "User")
                        .WithMany("tblUserGames")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("fk_tblUserGame_UserId");

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblGame", b =>
                {
                    b.Navigation("tblPieceGames");

                    b.Navigation("tblUserGames");
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblPiece", b =>
                {
                    b.Navigation("tblPieceGames");
                });

            modelBuilder.Entity("Trouble.PL.Entities.tblUser", b =>
                {
                    b.Navigation("tblUserGames");
                });
#pragma warning restore 612, 618
        }
    }
}
