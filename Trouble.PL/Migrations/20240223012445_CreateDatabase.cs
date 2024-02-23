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
                    { new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new DateTime(2024, 2, 22, 19, 24, 44, 746, DateTimeKind.Local).AddTicks(8427), "Game1", 0 },
                    { new Guid("373529ca-49a8-455a-b45d-e6e5ecc295ea"), new DateTime(2024, 2, 22, 19, 24, 44, 746, DateTimeKind.Local).AddTicks(8468), "Game2", 0 }
                });

            migrationBuilder.InsertData(
                table: "tblPiece",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { new Guid("010152c6-7f33-404e-94f0-7e8d1f62e47a"), "Green" },
                    { new Guid("078eb18a-9b7b-4582-a6d4-44030d628812"), "Blue" },
                    { new Guid("0c8ec1b3-2eff-4f99-8cfa-db7e8e9527d3"), "Red" },
                    { new Guid("28a6c166-5ed7-4186-81b2-bbaf7c9560bd"), "Blue" },
                    { new Guid("2bdf38d3-45f3-45a1-a81e-e66cc726a02d"), "Blue" },
                    { new Guid("4369f643-f157-4d34-8524-0b9cf4c0b7f6"), "Blue" },
                    { new Guid("445c9c75-0c69-4616-b68f-a4d07ab4b674"), "Yellow" },
                    { new Guid("48891ff0-b047-43f7-b638-52932c818704"), "Green" },
                    { new Guid("56d8efdc-d403-4a64-a6c5-bfbaed13aa4a"), "Yellow" },
                    { new Guid("61f13faa-7765-452f-aa76-c37c5126d2c9"), "Green" },
                    { new Guid("711485d1-1d0e-4943-9cdc-30493ffac938"), "Yellow" },
                    { new Guid("93681a93-a7b5-4924-a630-37bfcdf62429"), "Red" },
                    { new Guid("9cd75a94-0c62-4915-8212-8d58605608f4"), "Green" },
                    { new Guid("a0306e1e-f8eb-45c6-874b-fb6c01405630"), "Red" },
                    { new Guid("a2a208dd-34dd-4cc2-9384-75e86d1336f2"), "Red" },
                    { new Guid("fa200e39-0418-4a0c-81aa-559a9d90bdd9"), "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("2a004861-517d-400b-9665-d8b79225658e"), "Bob", "Bob", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User1" },
                    { new Guid("374350c0-48eb-4a9d-b1c6-8154c39e09db"), "Susan", "Susan", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User3" },
                    { new Guid("6911a7d5-eb1f-489e-8231-1126623c0ae5"), "Test", "Test", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User5" },
                    { new Guid("e45eca0c-5e5e-4cfd-a54a-db9af4d559ab"), "Sally", "Sally", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User4" },
                    { new Guid("f9301289-cf75-4af6-b468-a3b1bf051933"), "Joe", "Joe", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User2" }
                });

            migrationBuilder.InsertData(
                table: "tblPieceGame",
                columns: new[] { "Id", "GameId", "PieceId", "PieceLocation" },
                values: new object[,]
                {
                    { new Guid("02673ad7-cfe9-44bc-b0df-2deecf7086b3"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("711485d1-1d0e-4943-9cdc-30493ffac938"), 0 },
                    { new Guid("0b62745f-8112-47aa-82b5-820ce8c61517"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("56d8efdc-d403-4a64-a6c5-bfbaed13aa4a"), 0 },
                    { new Guid("0d1c9ce9-9ddf-4683-8e02-9e18c6247188"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("9cd75a94-0c62-4915-8212-8d58605608f4"), 0 },
                    { new Guid("1e182670-129b-4fba-8a5d-d2465c2e3ebe"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("a0306e1e-f8eb-45c6-874b-fb6c01405630"), 0 },
                    { new Guid("3bb10e12-895d-4f10-a447-f0466fa4eb2c"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("4369f643-f157-4d34-8524-0b9cf4c0b7f6"), 0 },
                    { new Guid("4594e3c9-e1ab-4c91-91d9-916749f11391"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("0c8ec1b3-2eff-4f99-8cfa-db7e8e9527d3"), 0 },
                    { new Guid("4771a29d-55cd-47d5-995b-345d678b05ce"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("2bdf38d3-45f3-45a1-a81e-e66cc726a02d"), 0 },
                    { new Guid("4dc9a535-4324-4b26-8a76-2ef931645694"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("078eb18a-9b7b-4582-a6d4-44030d628812"), 0 },
                    { new Guid("581bc473-3359-47fd-8f5e-b02c4593adc9"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("fa200e39-0418-4a0c-81aa-559a9d90bdd9"), 0 },
                    { new Guid("5b1515d2-c83d-4242-ab23-658f7bb05089"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("48891ff0-b047-43f7-b638-52932c818704"), 0 },
                    { new Guid("6cc710f1-2db6-4462-9965-ab4ac9375353"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("93681a93-a7b5-4924-a630-37bfcdf62429"), 0 },
                    { new Guid("87d98eb5-2450-44ae-a75a-4d96c2003d54"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("61f13faa-7765-452f-aa76-c37c5126d2c9"), 0 },
                    { new Guid("ba47ef09-4d7d-4444-b7ae-6cbaee86e801"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("28a6c166-5ed7-4186-81b2-bbaf7c9560bd"), 0 },
                    { new Guid("baf42e0e-0317-4acc-8485-a4a5f7d16ff5"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("010152c6-7f33-404e-94f0-7e8d1f62e47a"), 0 },
                    { new Guid("f8db3466-6c8b-416d-b22c-1a4acdc13cf1"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("a2a208dd-34dd-4cc2-9384-75e86d1336f2"), 0 },
                    { new Guid("fde3b46a-d053-4afa-aa13-d0471d467aa5"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("445c9c75-0c69-4616-b68f-a4d07ab4b674"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("1b6b1550-a495-4afa-af4a-6a260bb229d7"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("374350c0-48eb-4a9d-b1c6-8154c39e09db") },
                    { new Guid("65bcc509-9fcf-4876-8d5d-91c17c905c8d"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("f9301289-cf75-4af6-b468-a3b1bf051933") },
                    { new Guid("924fcd12-b885-49a4-8977-88fe0d1af712"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("e45eca0c-5e5e-4cfd-a54a-db9af4d559ab") },
                    { new Guid("ee0af0c9-7a1f-4914-95c0-d240c6a7b477"), new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"), new Guid("2a004861-517d-400b-9665-d8b79225658e") }
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
