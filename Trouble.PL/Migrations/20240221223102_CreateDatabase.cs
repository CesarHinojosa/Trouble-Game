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
                    Color = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false)
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
                    { new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new DateTime(2024, 2, 21, 16, 31, 2, 582, DateTimeKind.Local).AddTicks(9146), "Game1", 0 },
                    { new Guid("e29b528a-9562-4a16-b17a-669a3a943b83"), new DateTime(2024, 2, 21, 16, 31, 2, 582, DateTimeKind.Local).AddTicks(9189), "Game2", 0 }
                });

            migrationBuilder.InsertData(
                table: "tblPiece",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { new Guid("283b8fb8-a808-4910-8cde-03e37a8106a3"), "Blue" },
                    { new Guid("2c5aa89a-096b-4710-aa93-3b85d75450e1"), "Red" },
                    { new Guid("61967ec8-786f-4658-8052-d84f2c98a9be"), "Blue" },
                    { new Guid("68998b85-54ad-47db-91c0-8232038859d1"), "Yellow" },
                    { new Guid("6bf50076-784a-4b1a-b64e-7990a44ebbcb"), "Yellow" },
                    { new Guid("80685a4f-303c-412c-b959-7cfd785a266b"), "Green" },
                    { new Guid("807dd5bf-b423-4804-862c-446caa9854d7"), "Yellow" },
                    { new Guid("8669cfce-a9aa-4d6a-bc6d-85d0e159e168"), "Blue" },
                    { new Guid("ab205627-b31f-4d45-8eea-490d652bc59f"), "Red" },
                    { new Guid("b06811e8-0bac-4ced-9cf0-38ba3223371e"), "Red" },
                    { new Guid("b99a984a-96a7-422f-9c23-530c669a11cd"), "Blue" },
                    { new Guid("b9c3d624-46d6-44a9-82d7-53f288b612e6"), "Green" },
                    { new Guid("eed9b721-9bdc-4143-bd5a-cfb15b57ae94"), "Green" },
                    { new Guid("f5732f9c-4ebd-4ad0-97a4-0febca893068"), "Red" },
                    { new Guid("f7af0a28-f5e4-4360-b909-4d5769396ef6"), "Yellow" },
                    { new Guid("fbfd5327-b1f9-4bc2-987e-f1c2e4827b69"), "Green" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("372ebf81-af39-4807-a42d-adb7f79f18fd"), "Susan", "Susan", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User3" },
                    { new Guid("3d1a26ed-c3d1-4174-9ee8-96b8faa9f5f6"), "Bob", "Bob", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User1" },
                    { new Guid("c2ad14c8-4895-4e5d-a80b-de387f89bc61"), "Joe", "Joe", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User2" },
                    { new Guid("daa51d07-86f5-43f9-9640-97b2995fd72a"), "Sally", "Sally", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User4" }
                });

            migrationBuilder.InsertData(
                table: "tblPieceGame",
                columns: new[] { "Id", "GameId", "PieceId", "PieceLocation" },
                values: new object[,]
                {
                    { new Guid("014105d7-3d41-4f05-8d2c-d2613db5f969"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("ab205627-b31f-4d45-8eea-490d652bc59f"), 0 },
                    { new Guid("28af336a-c88b-47ef-9484-fbb1d2fa6f17"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("2c5aa89a-096b-4710-aa93-3b85d75450e1"), 0 },
                    { new Guid("3a9be791-7898-4d32-8da5-d34022ae00c7"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("68998b85-54ad-47db-91c0-8232038859d1"), 0 },
                    { new Guid("3e17b9a4-2c44-4d38-8f83-4c83025ef456"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("b9c3d624-46d6-44a9-82d7-53f288b612e6"), 0 },
                    { new Guid("3e2be232-4e85-44a8-874f-38c69961b169"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("f5732f9c-4ebd-4ad0-97a4-0febca893068"), 0 },
                    { new Guid("430b51ff-5db1-429a-8584-07b27f4ba3eb"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("6bf50076-784a-4b1a-b64e-7990a44ebbcb"), 0 },
                    { new Guid("688ad089-f2d1-4d41-9485-a497d0d95dff"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("f7af0a28-f5e4-4360-b909-4d5769396ef6"), 0 },
                    { new Guid("7da056fa-b7cc-4960-95b9-e347e4c1454f"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("61967ec8-786f-4658-8052-d84f2c98a9be"), 0 },
                    { new Guid("7ed43aaa-4807-487d-9034-9abf2a25d8a1"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("283b8fb8-a808-4910-8cde-03e37a8106a3"), 0 },
                    { new Guid("897d54f7-baf7-447f-bd77-83f9c372e92b"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("b06811e8-0bac-4ced-9cf0-38ba3223371e"), 0 },
                    { new Guid("a7c6d160-2cc4-4564-9ae5-0a29d51ac0eb"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("b99a984a-96a7-422f-9c23-530c669a11cd"), 0 },
                    { new Guid("b853f5a6-2b88-4138-a745-6122c5ea6f73"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("8669cfce-a9aa-4d6a-bc6d-85d0e159e168"), 0 },
                    { new Guid("bfbc81b9-bc40-42a4-a1c3-7a8991fb9128"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("80685a4f-303c-412c-b959-7cfd785a266b"), 0 },
                    { new Guid("cd6af0b0-0b13-411f-9ffc-b91b04c09c33"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("807dd5bf-b423-4804-862c-446caa9854d7"), 0 },
                    { new Guid("e4482f83-52d7-4f16-8892-f17e300e4455"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("eed9b721-9bdc-4143-bd5a-cfb15b57ae94"), 0 },
                    { new Guid("f240439b-a0d1-4e5e-bdb2-e12c50f06703"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("fbfd5327-b1f9-4bc2-987e-f1c2e4827b69"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0c52e982-20a1-44f1-b459-a03796b5fe2d"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("372ebf81-af39-4807-a42d-adb7f79f18fd") },
                    { new Guid("232ce084-972d-4af9-ba37-cad887afb825"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("3d1a26ed-c3d1-4174-9ee8-96b8faa9f5f6") },
                    { new Guid("62279864-1d34-4734-a2de-be865dd7b02a"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("c2ad14c8-4895-4e5d-a80b-de387f89bc61") },
                    { new Guid("f7b46b42-0ae9-40f3-bac4-1c7d2ce868dc"), new Guid("0d663fbd-3e5e-41fd-95aa-ad04500f8ec0"), new Guid("daa51d07-86f5-43f9-9640-97b2995fd72a") }
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
