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
                    { new Guid("c6337b55-4370-4965-9508-34de26361787"), new DateTime(2024, 2, 22, 14, 47, 35, 201, DateTimeKind.Local).AddTicks(5507), "Game1", 0 },
                    { new Guid("ee20e0c2-9433-407a-9e88-be95273b9f96"), new DateTime(2024, 2, 22, 14, 47, 35, 201, DateTimeKind.Local).AddTicks(5561), "Game2", 0 }
                });

            migrationBuilder.InsertData(
                table: "tblPiece",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { new Guid("142f44e8-fde7-426b-ab5d-8ac2837b62b5"), "Blue" },
                    { new Guid("16ca0950-3ed6-4c9f-a6fd-6dfdc1917979"), "Red" },
                    { new Guid("1db5564f-e1e4-4810-9078-7f51768d1afa"), "Blue" },
                    { new Guid("207af9d3-fae3-4f67-8346-b0a16a313c4b"), "Red" },
                    { new Guid("3f03e840-0523-43e5-b723-1f386edc8c6e"), "Yellow" },
                    { new Guid("4cca7de6-1658-407d-9f45-4387ceca01b1"), "Red" },
                    { new Guid("588e54b6-596c-430f-aee0-0f46cdb31ed1"), "Green" },
                    { new Guid("6820cfc6-2f41-46c5-ae4d-4af90c542509"), "Blue" },
                    { new Guid("6eb61891-d0a8-4ce1-ba81-05543abb8451"), "Blue" },
                    { new Guid("708b722f-235a-4962-ba50-e9187e5d1a7f"), "Green" },
                    { new Guid("9cc5765e-6be9-4010-ba98-11dafd6c94aa"), "Yellow" },
                    { new Guid("a6f181e3-257e-467d-9e88-889726693835"), "Green" },
                    { new Guid("cc9a1056-056d-42f5-bc0d-8a909fc628b5"), "Green" },
                    { new Guid("d03ed69a-1acb-41f1-9bac-73619d30dcf4"), "Yellow" },
                    { new Guid("d8df6cf8-98e4-4805-baed-488aeef42f63"), "Red" },
                    { new Guid("e419ed30-b259-4816-a2f0-889bef73fd65"), "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("0be3986f-ad9b-4d96-8b44-1605704f825b"), "Bob", "Bob", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User1" },
                    { new Guid("a7d0b8c1-de62-45eb-b725-769d10a09c1b"), "Joe", "Joe", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User2" },
                    { new Guid("d3b02c06-95cb-476a-871b-bc829b6fba88"), "Susan", "Susan", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User3" },
                    { new Guid("e473cd68-620e-40d3-90b8-d7cecd019674"), "Sally", "Sally", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User4" }
                });

            migrationBuilder.InsertData(
                table: "tblPieceGame",
                columns: new[] { "Id", "GameId", "PieceId", "PieceLocation" },
                values: new object[,]
                {
                    { new Guid("07cbae2e-700b-44f9-bb41-360f95355b74"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("1db5564f-e1e4-4810-9078-7f51768d1afa"), 0 },
                    { new Guid("39cefd86-0b50-4d03-853f-fdd7c551783d"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("3f03e840-0523-43e5-b723-1f386edc8c6e"), 0 },
                    { new Guid("65723929-0c54-4fd1-8e11-f596fdcb2451"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("16ca0950-3ed6-4c9f-a6fd-6dfdc1917979"), 0 },
                    { new Guid("7001b39f-96f4-4694-9cdc-dcd7067c144f"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("d8df6cf8-98e4-4805-baed-488aeef42f63"), 0 },
                    { new Guid("703a85e2-dc17-443f-8ffb-2dc283ca6a43"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("4cca7de6-1658-407d-9f45-4387ceca01b1"), 0 },
                    { new Guid("748c875f-6f1e-43db-a306-9dd11788fef8"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("e419ed30-b259-4816-a2f0-889bef73fd65"), 0 },
                    { new Guid("7797ed61-ce9d-46b9-8469-7bd20421c9c2"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("a6f181e3-257e-467d-9e88-889726693835"), 0 },
                    { new Guid("9bf27216-d6f8-43a1-94e7-173185d684a4"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("142f44e8-fde7-426b-ab5d-8ac2837b62b5"), 0 },
                    { new Guid("a2c01408-5202-4739-813d-84fc1276859a"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("708b722f-235a-4962-ba50-e9187e5d1a7f"), 0 },
                    { new Guid("b7a48085-e93d-4203-b76e-70479e61c0ba"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("9cc5765e-6be9-4010-ba98-11dafd6c94aa"), 0 },
                    { new Guid("b8cbb831-eb9a-44e0-be89-40dbda955411"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("cc9a1056-056d-42f5-bc0d-8a909fc628b5"), 0 },
                    { new Guid("cce42d14-1250-4980-8b20-38fd7e205238"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("d03ed69a-1acb-41f1-9bac-73619d30dcf4"), 0 },
                    { new Guid("de2bcdc7-34e3-41a7-b30d-9b7a637c9007"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("207af9d3-fae3-4f67-8346-b0a16a313c4b"), 0 },
                    { new Guid("edab3310-2a1d-497a-9e39-e0a0721257e5"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("588e54b6-596c-430f-aee0-0f46cdb31ed1"), 0 },
                    { new Guid("fa894c25-2e93-45c0-a954-9692bb3130a3"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("6eb61891-d0a8-4ce1-ba81-05543abb8451"), 0 },
                    { new Guid("ff805459-5187-44d9-9ce2-851424322fc2"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("6820cfc6-2f41-46c5-ae4d-4af90c542509"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("9c799434-4b49-4393-ae77-9133379fdbdf"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("e473cd68-620e-40d3-90b8-d7cecd019674") },
                    { new Guid("abd9d242-08a2-4a85-bcc5-00d26dcc8179"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("0be3986f-ad9b-4d96-8b44-1605704f825b") },
                    { new Guid("b173598c-1c84-4b18-8f8a-7ce1ecafad48"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("d3b02c06-95cb-476a-871b-bc829b6fba88") },
                    { new Guid("ecf930ae-b52d-4ebe-ac91-614f0c20ab63"), new Guid("c6337b55-4370-4965-9508-34de26361787"), new Guid("a7d0b8c1-de62-45eb-b725-769d10a09c1b") }
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
