using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trouble.PL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurnNum = table.Column<int>(type: "int", nullable: false),
                    GameName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGame_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPiece",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPiece_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(28)", unicode: false, maxLength: 28, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPieceGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PieceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PieceLocation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPieceGame_Id", x => x.Id);
                    table.ForeignKey(
                        name: "tblPieceGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "tblPieceGame_PieceId",
                        column: x => x.PieceId,
                        principalTable: "tblPiece",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserGame_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_tblUserGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_tblUserGame_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "GameDate", "GameName", "TurnNum" },
                values: new object[,]
                {
                    { new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new DateTime(2024, 2, 22, 18, 27, 44, 894, DateTimeKind.Local).AddTicks(4289), "Game1", 0 },
                    { new Guid("c1fb3ac8-f030-463c-96c1-50b697b9c534"), new DateTime(2024, 2, 22, 18, 27, 44, 894, DateTimeKind.Local).AddTicks(4330), "Game2", 0 }
                });

            migrationBuilder.InsertData(
                table: "tblPiece",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { new Guid("069e4efa-10b3-4727-9cf7-083d96e9565c"), "Green" },
                    { new Guid("15f7c95e-85df-4f1f-9483-dc1206f9a49d"), "Yellow" },
                    { new Guid("1b9dcf9a-af53-4ade-8642-24ae7e73b8bd"), "Blue" },
                    { new Guid("3341930b-0940-4dc1-9e3b-ef83da5e4c1d"), "Blue" },
                    { new Guid("5596cd68-0b52-4bb7-86d0-4f9d1c03ffb1"), "Green" },
                    { new Guid("74520448-c375-4ee9-ba6f-07ebf65c8136"), "Blue" },
                    { new Guid("77243022-b823-406f-924f-6164e4321eca"), "Blue" },
                    { new Guid("7bd521ce-1af8-4d5d-8b94-7599a222c5e8"), "Red" },
                    { new Guid("7d3f05f2-7b7e-461c-9ec8-9365a6ea8f81"), "Yellow" },
                    { new Guid("846435a1-4329-4d82-8c67-24ac95bb214d"), "Green" },
                    { new Guid("a7933241-0931-4537-8516-2bb56bdb69d4"), "Green" },
                    { new Guid("cdc863ab-47fb-49b5-babf-319213a168e8"), "Red" },
                    { new Guid("d1d95e3f-641a-4b8e-adb0-29c73069b7bf"), "Yellow" },
                    { new Guid("d50d0698-d19d-4fcc-bedb-cffe17283bd2"), "Red" },
                    { new Guid("d560299a-ce8b-4296-b66d-e8df03fdb258"), "Yellow" },
                    { new Guid("f6d8a402-4d63-4202-a23a-c519377d0a23"), "Red" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("4df8fe47-9d3f-4c75-ab9e-a9f1970acf2b"), "Bob", "Bob", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User1" },
                    { new Guid("722246c2-1979-4e57-a6b5-28269210d6e5"), "Susan", "Susan", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User3" },
                    { new Guid("7e05634a-a83b-4ef1-9f11-ebda6bcbca12"), "Joe", "Joe", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User2" },
                    { new Guid("ab249eb8-ec98-4dd3-83a6-e2761cfe3001"), "Test", "Test", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User5" },
                    { new Guid("d73c567d-66e5-4998-8757-7ac78f270e00"), "Sally", "Sally", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User4" }
                });

            migrationBuilder.InsertData(
                table: "tblPieceGame",
                columns: new[] { "Id", "GameId", "PieceId", "PieceLocation" },
                values: new object[,]
                {
                    { new Guid("004fdff7-f39e-494b-90fb-242439680b58"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("15f7c95e-85df-4f1f-9483-dc1206f9a49d"), 0 },
                    { new Guid("0274f949-caf7-4645-932f-ce9e7b733d7b"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("5596cd68-0b52-4bb7-86d0-4f9d1c03ffb1"), 0 },
                    { new Guid("0f0838d2-17ae-455c-b245-e66a7d68d19d"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("d1d95e3f-641a-4b8e-adb0-29c73069b7bf"), 0 },
                    { new Guid("2f08b679-0133-4ecb-85c7-3fbb5458c5a0"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("1b9dcf9a-af53-4ade-8642-24ae7e73b8bd"), 0 },
                    { new Guid("449eee02-5494-402b-a7e9-86a129d3560b"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("74520448-c375-4ee9-ba6f-07ebf65c8136"), 0 },
                    { new Guid("4c08c194-1adb-459a-8674-59adb1b2d135"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("d560299a-ce8b-4296-b66d-e8df03fdb258"), 0 },
                    { new Guid("643aaae8-ef23-4932-967b-c9df34b9fe7d"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("cdc863ab-47fb-49b5-babf-319213a168e8"), 0 },
                    { new Guid("6dd59b06-0907-47d5-a923-547458c73dfb"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("846435a1-4329-4d82-8c67-24ac95bb214d"), 0 },
                    { new Guid("719c8975-8f95-4ba9-9d3f-0b3d81ce1143"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("7d3f05f2-7b7e-461c-9ec8-9365a6ea8f81"), 0 },
                    { new Guid("836b5140-9f2c-4c81-b324-e6f5ef647f1b"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("a7933241-0931-4537-8516-2bb56bdb69d4"), 0 },
                    { new Guid("9265f1c5-8937-4fa9-b015-c74e4541ef01"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("7bd521ce-1af8-4d5d-8b94-7599a222c5e8"), 0 },
                    { new Guid("9ed51244-1822-454e-b223-ecf4955d30a6"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("d50d0698-d19d-4fcc-bedb-cffe17283bd2"), 0 },
                    { new Guid("b29d5c07-5817-4388-ae6f-fbced3ea2f8a"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("3341930b-0940-4dc1-9e3b-ef83da5e4c1d"), 0 },
                    { new Guid("b70eae81-f04f-4087-8f6f-b83d1171f38e"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("f6d8a402-4d63-4202-a23a-c519377d0a23"), 0 },
                    { new Guid("cc118926-adfb-4a72-b692-6df84ed93855"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("77243022-b823-406f-924f-6164e4321eca"), 0 },
                    { new Guid("d8bee404-e0f6-4226-b963-18560b388968"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("069e4efa-10b3-4727-9cf7-083d96e9565c"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("703d999d-42f6-4b93-8f08-ce3bb0d8af08"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("7e05634a-a83b-4ef1-9f11-ebda6bcbca12") },
                    { new Guid("79c120db-4866-4ea8-8f5f-af819652105a"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("722246c2-1979-4e57-a6b5-28269210d6e5") },
                    { new Guid("ac1e7d6e-45e0-4d90-830c-f939e48913ee"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("4df8fe47-9d3f-4c75-ab9e-a9f1970acf2b") },
                    { new Guid("dfa059f4-f78a-4461-aa87-c62e41c7c82e"), new Guid("9f6f4f9b-e52d-4ffa-8ae2-a42af7f3d1b5"), new Guid("d73c567d-66e5-4998-8757-7ac78f270e00") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPieceGame_GameId",
                table: "tblPieceGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPieceGame_PieceId",
                table: "tblPieceGame",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_GameId",
                table: "tblUserGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_UserId",
                table: "tblUserGame",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPieceGame");

            migrationBuilder.DropTable(
                name: "tblUserGame");

            migrationBuilder.DropTable(
                name: "tblPiece");

            migrationBuilder.DropTable(
                name: "tblGame");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
