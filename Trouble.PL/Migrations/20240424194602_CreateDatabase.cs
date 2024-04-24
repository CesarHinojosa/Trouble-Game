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
                    { new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new DateTime(2024, 4, 24, 14, 46, 1, 516, DateTimeKind.Local).AddTicks(4078), "Game1", 0 },
                    { new Guid("ffb33984-9bf9-4b4c-9668-c9883c780a83"), new DateTime(2024, 4, 24, 14, 46, 1, 516, DateTimeKind.Local).AddTicks(4114), "Game2", 0 }
                });

            migrationBuilder.InsertData(
                table: "tblPiece",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { new Guid("09e29f5d-1482-480d-8da1-9ab63fb69f7f"), "Red" },
                    { new Guid("201c3dbc-a481-4634-b353-f51708250be2"), "Green" },
                    { new Guid("351d2af1-21af-4ec5-8ce2-e7e5589d9769"), "Yellow" },
                    { new Guid("3eba5e4b-fbbe-484f-856d-c15e19f3c0c4"), "Green" },
                    { new Guid("42387750-418a-4f64-89ff-74b0c39f2b26"), "Red" },
                    { new Guid("4ea9180d-4603-4893-aea1-6f8dac7cfb37"), "Green" },
                    { new Guid("57406578-e967-4fad-bf96-23357089199e"), "Blue" },
                    { new Guid("6c634491-9525-4cd8-bccd-5577e33598bf"), "Blue" },
                    { new Guid("7ea1db8a-7199-4d24-a4cd-aa1d8b7d71a3"), "Yellow" },
                    { new Guid("83ee0fcb-75ec-420e-a777-5614b9d47da3"), "Yellow" },
                    { new Guid("9f1bbe17-debb-4882-9726-6831cdf56100"), "Yellow" },
                    { new Guid("af39def9-b6a5-45bc-ab46-ac572807a8c9"), "Green" },
                    { new Guid("cb11515d-5b07-4180-9533-3df19aab7ee2"), "Red" },
                    { new Guid("cdbbf73c-2775-41ee-9acf-a84a98af1d6e"), "Blue" },
                    { new Guid("e991a08a-34d7-467a-8deb-a3d8e82c2fec"), "Blue" },
                    { new Guid("fc9c24aa-8efa-43e3-9e8d-83338625372f"), "Red" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("0e65103e-af27-4657-9a65-2a21957d55f9"), "Bob", "Bob", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User1" },
                    { new Guid("34b435a4-1c4c-4671-987f-ca0d6d51cd95"), "Susan", "Susan", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User3" },
                    { new Guid("525d567f-dbbd-4576-b4ea-eb9a3452c0e1"), "Sally", "Sally", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User4" },
                    { new Guid("921b3ceb-a6c6-43e1-a353-d8a4f3047412"), "Test", "Test", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User5" },
                    { new Guid("e03f19b7-e8aa-4b73-8b74-e1baa5532b58"), "Joe", "Joe", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User2" }
                });

            migrationBuilder.InsertData(
                table: "tblPieceGame",
                columns: new[] { "Id", "GameId", "PieceId", "PieceLocation" },
                values: new object[,]
                {
                    { new Guid("02fa0cf7-5cb2-49c3-921b-eb5481a500c7"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("cdbbf73c-2775-41ee-9acf-a84a98af1d6e"), 0 },
                    { new Guid("0f6c19cf-8063-466d-9207-c0f75712c527"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("3eba5e4b-fbbe-484f-856d-c15e19f3c0c4"), 0 },
                    { new Guid("1df61e2a-25d8-49a2-af52-fd8f67689405"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("af39def9-b6a5-45bc-ab46-ac572807a8c9"), 0 },
                    { new Guid("2bc05284-51b0-45a4-af35-1d563a5d392f"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("9f1bbe17-debb-4882-9726-6831cdf56100"), 0 },
                    { new Guid("49014f9f-33e6-409c-9f19-5559d9b9fb87"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("351d2af1-21af-4ec5-8ce2-e7e5589d9769"), 0 },
                    { new Guid("5e0a857c-8bf7-4a19-8c26-b2746260d0c6"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("e991a08a-34d7-467a-8deb-a3d8e82c2fec"), 0 },
                    { new Guid("7511583b-1a94-44de-b5d2-fa1f1195ef38"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("42387750-418a-4f64-89ff-74b0c39f2b26"), 5 },
                    { new Guid("7c3402c8-2ac4-443e-9e2f-37b28f6a98eb"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("201c3dbc-a481-4634-b353-f51708250be2"), 0 },
                    { new Guid("8288ad83-e67a-46b3-b8d3-36fa94b3686a"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("cb11515d-5b07-4180-9533-3df19aab7ee2"), 2 },
                    { new Guid("899acd33-9d67-4dc2-b7b4-a8508959e1df"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("6c634491-9525-4cd8-bccd-5577e33598bf"), 0 },
                    { new Guid("922cbef6-64ef-4cc6-9bc7-ab19ac2a7ec3"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("4ea9180d-4603-4893-aea1-6f8dac7cfb37"), 0 },
                    { new Guid("bc54024f-39bd-46be-bae7-5426a94a4b71"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("7ea1db8a-7199-4d24-a4cd-aa1d8b7d71a3"), 0 },
                    { new Guid("c87f3b50-58d1-4f3f-bd25-4ea6d2076779"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("83ee0fcb-75ec-420e-a777-5614b9d47da3"), 0 },
                    { new Guid("cf456720-51e3-4112-bec7-e94da7adbc35"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("57406578-e967-4fad-bf96-23357089199e"), 0 },
                    { new Guid("d215dc4a-bdd2-4dd1-85eb-35528d197d73"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("fc9c24aa-8efa-43e3-9e8d-83338625372f"), 0 },
                    { new Guid("ed48f325-83a2-48dc-b34d-b35c3183f93f"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("09e29f5d-1482-480d-8da1-9ab63fb69f7f"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d23ab6b-10a2-4f4e-a02f-a5f1db12690d"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("34b435a4-1c4c-4671-987f-ca0d6d51cd95") },
                    { new Guid("52669b8e-34b2-496e-9564-ef46e3baac50"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("0e65103e-af27-4657-9a65-2a21957d55f9") },
                    { new Guid("d41e79f8-cb12-4efe-8d13-18ad9678330b"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("525d567f-dbbd-4576-b4ea-eb9a3452c0e1") },
                    { new Guid("da9c5fb9-e55e-42d3-b666-c00a1ea004c1"), new Guid("d20228c1-e0b5-4dc7-b7dd-014414397feb"), new Guid("e03f19b7-e8aa-4b73-8b74-e1baa5532b58") }
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
