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
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerColor = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
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
                    { new Guid("05efc620-8508-4381-9ab4-369af1daa2c9"), new DateTime(2024, 4, 26, 12, 55, 15, 676, DateTimeKind.Local).AddTicks(8201), "Game2", 0 },
                    { new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new DateTime(2024, 4, 26, 12, 55, 15, 676, DateTimeKind.Local).AddTicks(8143), "Game1", 0 }
                });

            migrationBuilder.InsertData(
                table: "tblPiece",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { new Guid("0b45da73-d52b-461d-9e30-6b26e736328a"), "Green" },
                    { new Guid("0d4a5ade-d268-485d-8372-52cbda3a90a1"), "Red" },
                    { new Guid("1927ba14-e4f6-4c55-b554-40c87d2a401e"), "Blue" },
                    { new Guid("195b292c-0605-484f-a5b4-4f6c4e0db940"), "Yellow" },
                    { new Guid("262a020a-e281-4e5f-ac3d-b14798168353"), "Blue" },
                    { new Guid("448e3e9c-7f34-4685-ab4d-27d25d5ad977"), "Blue" },
                    { new Guid("44fe7a64-ef08-4f19-9387-dc842c7eabb8"), "Red" },
                    { new Guid("5186f3c8-a537-4715-a97e-37961a381204"), "Green" },
                    { new Guid("953489bc-0c5b-423b-a230-eb1522da8a28"), "Yellow" },
                    { new Guid("9756029c-1732-479c-a948-91539b2765e2"), "Green" },
                    { new Guid("98719298-427c-412f-b01d-ff0acae7a0ba"), "Red" },
                    { new Guid("d311b97c-5054-4f24-83ad-e67d49755d8b"), "Red" },
                    { new Guid("e5abdfde-17e6-45ef-a8cc-55f386045a12"), "Green" },
                    { new Guid("e7469172-7a5b-4de2-8180-e99edf430dd4"), "Yellow" },
                    { new Guid("f6b8e1ba-bf6c-462d-ade4-8d9241bb5d5d"), "Blue" },
                    { new Guid("f7b7eb7c-0c1d-4d52-a446-fcad4a0d1f8c"), "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("07967984-ace6-41c3-899b-92eac5ffb8f6"), "Bob", "Bob", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User1" },
                    { new Guid("3677e32d-de1f-48af-9c93-57b321836935"), "Sally", "Sally", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User4" },
                    { new Guid("38759d29-5ec7-4715-9017-58529fdb917b"), "Susan", "Susan", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User3" },
                    { new Guid("9737d11b-0afa-4760-bfa8-bec07ed4e77a"), "Test", "Test", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User5" },
                    { new Guid("bed2c8c8-2363-4d4a-b72b-ca23e959a61b"), "Joe", "Joe", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User2" }
                });

            migrationBuilder.InsertData(
                table: "tblPieceGame",
                columns: new[] { "Id", "GameId", "PieceId", "PieceLocation" },
                values: new object[,]
                {
                    { new Guid("06224729-76d1-493f-a99a-05a681061bd6"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("195b292c-0605-484f-a5b4-4f6c4e0db940"), 0 },
                    { new Guid("203ceed2-e4ba-4aa4-886d-b25d5a972b70"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("d311b97c-5054-4f24-83ad-e67d49755d8b"), 0 },
                    { new Guid("2119aa36-e72f-4267-84cf-9618ecfe0b21"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("448e3e9c-7f34-4685-ab4d-27d25d5ad977"), 0 },
                    { new Guid("2b24976e-5ef9-4a90-b1d0-45a30088afa5"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("9756029c-1732-479c-a948-91539b2765e2"), 0 },
                    { new Guid("2b6a1066-2a76-4d94-b589-172bb5996730"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("e5abdfde-17e6-45ef-a8cc-55f386045a12"), 0 },
                    { new Guid("3ce67266-6b23-4b2d-aa33-e7e19c304e6b"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("953489bc-0c5b-423b-a230-eb1522da8a28"), 0 },
                    { new Guid("6fdfc1e6-254b-469a-a13f-97d9015df0fc"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("f7b7eb7c-0c1d-4d52-a446-fcad4a0d1f8c"), 0 },
                    { new Guid("755e3dd4-a138-4fa0-9530-6bcb10927a51"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("0b45da73-d52b-461d-9e30-6b26e736328a"), 0 },
                    { new Guid("86ac8a0d-db87-4a13-980d-7fff628169b5"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("44fe7a64-ef08-4f19-9387-dc842c7eabb8"), 2 },
                    { new Guid("a348a6b5-1945-4167-831a-43fac45f5bd2"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("e7469172-7a5b-4de2-8180-e99edf430dd4"), 0 },
                    { new Guid("a5a47f61-2b97-4c64-ad6f-ee67dd9cae4d"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("5186f3c8-a537-4715-a97e-37961a381204"), 0 },
                    { new Guid("cd2894f8-3d8e-4522-a844-5e50a3398f3e"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("262a020a-e281-4e5f-ac3d-b14798168353"), 0 },
                    { new Guid("cf9ea10e-2ec9-40c7-8a0f-55a4a7ae177f"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("f6b8e1ba-bf6c-462d-ade4-8d9241bb5d5d"), 0 },
                    { new Guid("e8b4a464-6888-4c0a-ac0b-23cc25763028"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("98719298-427c-412f-b01d-ff0acae7a0ba"), 5 },
                    { new Guid("f07b553a-6f46-4ed1-b28f-dda5b55709c2"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("1927ba14-e4f6-4c55-b554-40c87d2a401e"), 0 },
                    { new Guid("f83f2c70-5cbc-4e9e-9a61-b42b01cf605b"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), new Guid("0d4a5ade-d268-485d-8372-52cbda3a90a1"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "PlayerColor", "UserId" },
                values: new object[,]
                {
                    { new Guid("52aaf549-51db-461e-bd9e-23aa0f598712"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), "Green", new Guid("07967984-ace6-41c3-899b-92eac5ffb8f6") },
                    { new Guid("73f4652e-6dc0-49ca-ad33-cecf7365509f"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), "Blue", new Guid("38759d29-5ec7-4715-9017-58529fdb917b") },
                    { new Guid("87a71e8d-7509-4360-becf-5d9e112b52e4"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), "Yellow", new Guid("bed2c8c8-2363-4d4a-b72b-ca23e959a61b") },
                    { new Guid("ff3a296b-7d53-4f00-b051-7336f5ea41cf"), new Guid("3d02117a-4051-460a-ba4d-baf5d4e583be"), "Red", new Guid("3677e32d-de1f-48af-9c93-57b321836935") }
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
