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
            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("373529ca-49a8-455a-b45d-e6e5ecc295ea"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("02673ad7-cfe9-44bc-b0df-2deecf7086b3"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("0b62745f-8112-47aa-82b5-820ce8c61517"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("0d1c9ce9-9ddf-4683-8e02-9e18c6247188"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("1e182670-129b-4fba-8a5d-d2465c2e3ebe"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("3bb10e12-895d-4f10-a447-f0466fa4eb2c"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("4594e3c9-e1ab-4c91-91d9-916749f11391"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("4771a29d-55cd-47d5-995b-345d678b05ce"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("4dc9a535-4324-4b26-8a76-2ef931645694"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("581bc473-3359-47fd-8f5e-b02c4593adc9"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("5b1515d2-c83d-4242-ab23-658f7bb05089"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("6cc710f1-2db6-4462-9965-ab4ac9375353"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("87d98eb5-2450-44ae-a75a-4d96c2003d54"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("ba47ef09-4d7d-4444-b7ae-6cbaee86e801"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("baf42e0e-0317-4acc-8485-a4a5f7d16ff5"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("f8db3466-6c8b-416d-b22c-1a4acdc13cf1"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("fde3b46a-d053-4afa-aa13-d0471d467aa5"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("6911a7d5-eb1f-489e-8231-1126623c0ae5"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("1b6b1550-a495-4afa-af4a-6a260bb229d7"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("65bcc509-9fcf-4876-8d5d-91c17c905c8d"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("924fcd12-b885-49a4-8977-88fe0d1af712"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("ee0af0c9-7a1f-4914-95c0-d240c6a7b477"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("00c76cb1-3c3f-44b1-a7e3-a64df5e4a11e"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("010152c6-7f33-404e-94f0-7e8d1f62e47a"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("078eb18a-9b7b-4582-a6d4-44030d628812"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("0c8ec1b3-2eff-4f99-8cfa-db7e8e9527d3"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("28a6c166-5ed7-4186-81b2-bbaf7c9560bd"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("2bdf38d3-45f3-45a1-a81e-e66cc726a02d"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("4369f643-f157-4d34-8524-0b9cf4c0b7f6"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("445c9c75-0c69-4616-b68f-a4d07ab4b674"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("48891ff0-b047-43f7-b638-52932c818704"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("56d8efdc-d403-4a64-a6c5-bfbaed13aa4a"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("61f13faa-7765-452f-aa76-c37c5126d2c9"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("711485d1-1d0e-4943-9cdc-30493ffac938"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("93681a93-a7b5-4924-a630-37bfcdf62429"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("9cd75a94-0c62-4915-8212-8d58605608f4"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("a0306e1e-f8eb-45c6-874b-fb6c01405630"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("a2a208dd-34dd-4cc2-9384-75e86d1336f2"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("fa200e39-0418-4a0c-81aa-559a9d90bdd9"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("2a004861-517d-400b-9665-d8b79225658e"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("374350c0-48eb-4a9d-b1c6-8154c39e09db"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("e45eca0c-5e5e-4cfd-a54a-db9af4d559ab"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("f9301289-cf75-4af6-b468-a3b1bf051933"));

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "GameDate", "GameName", "TurnNum" },
                values: new object[,]
                {
                    { new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new DateTime(2024, 3, 21, 17, 25, 50, 353, DateTimeKind.Local).AddTicks(7909), "Game1", 0 },
                    { new Guid("fac944a2-36cd-43ff-9c21-8c63a90e6160"), new DateTime(2024, 3, 21, 17, 25, 50, 353, DateTimeKind.Local).AddTicks(7957), "Game2", 0 }
                });

            migrationBuilder.InsertData(
                table: "tblPiece",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { new Guid("04605c78-c0f0-4713-869d-96235dadea16"), "Green" },
                    { new Guid("0e041e67-51c3-4d6e-a7b8-3684c5a9a793"), "Green" },
                    { new Guid("10279fe5-a3dd-4770-afb6-82a4f350cea5"), "Blue" },
                    { new Guid("1f3f10b6-9b51-4e53-b478-561a0f55fefc"), "Red" },
                    { new Guid("27fbed5f-b6de-4b93-9a4d-6eaae598d157"), "Blue" },
                    { new Guid("33231a13-5152-4ead-b34f-7c903d4bac54"), "Green" },
                    { new Guid("42b17ef7-36cf-4a45-b1b7-5595796565cc"), "Yellow" },
                    { new Guid("5fe75ac6-a73e-432c-9c17-6095af2d4aef"), "Red" },
                    { new Guid("6c2adcf3-7546-459d-a231-bcd0796624bb"), "Yellow" },
                    { new Guid("904bc09b-d190-4745-a667-d2eac333bcfe"), "Red" },
                    { new Guid("9c041be6-0049-4060-8c3d-0a1711dff500"), "Blue" },
                    { new Guid("b2430105-0289-45fc-95fc-7115c14393ed"), "Red" },
                    { new Guid("b800a56e-863d-4cfc-997b-5ecad5c7c92b"), "Yellow" },
                    { new Guid("d21b8cd5-ec32-4893-96e4-199312480fa6"), "Blue" },
                    { new Guid("d556e30c-6d87-48c3-97c9-3ae6335a2cb0"), "Green" },
                    { new Guid("f479f108-fffb-472a-80b2-3d4b00b7b4d0"), "Yellow" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("29a8209c-bc1e-45fb-975a-b3be403c8eb7"), "Test", "Test", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User5" },
                    { new Guid("4aef32ba-570e-449d-b1fc-14f18c2823aa"), "Susan", "Susan", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User3" },
                    { new Guid("7b5db418-8974-4dae-9d39-c44aa4bc08c3"), "Joe", "Joe", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User2" },
                    { new Guid("83063991-18b4-431b-9944-39e1d41c104d"), "Bob", "Bob", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User1" },
                    { new Guid("886eef82-fe91-4483-a32c-8b10f0a569be"), "Sally", "Sally", "ZAqyuuB77cTBY/Z5p0b3q3+10fo=", "User4" }
                });

            migrationBuilder.InsertData(
                table: "tblPieceGame",
                columns: new[] { "Id", "GameId", "PieceId", "PieceLocation" },
                values: new object[,]
                {
                    { new Guid("0cc3cca0-0921-4db7-adea-fe9239a93f24"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("1f3f10b6-9b51-4e53-b478-561a0f55fefc"), 2 },
                    { new Guid("1405a4e3-2a27-4c95-b2e7-b6c308f122b3"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("f479f108-fffb-472a-80b2-3d4b00b7b4d0"), 0 },
                    { new Guid("19ad4e4c-066e-4c80-b826-c9508e94c295"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("d21b8cd5-ec32-4893-96e4-199312480fa6"), 0 },
                    { new Guid("452219fe-a33f-4324-ba70-68fc7c07f948"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("6c2adcf3-7546-459d-a231-bcd0796624bb"), 0 },
                    { new Guid("588a0cf6-5ca6-4d54-8aec-1d49ed748e2d"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("904bc09b-d190-4745-a667-d2eac333bcfe"), 0 },
                    { new Guid("878e9d11-48be-4759-8762-e6a71a9eee0d"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("5fe75ac6-a73e-432c-9c17-6095af2d4aef"), 5 },
                    { new Guid("986f4b4e-63c7-4c8d-b301-524463eade8e"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("d556e30c-6d87-48c3-97c9-3ae6335a2cb0"), 0 },
                    { new Guid("9d6c13e4-9c37-4462-b155-34fc40e1727d"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("04605c78-c0f0-4713-869d-96235dadea16"), 0 },
                    { new Guid("a95d0af4-6a9b-4504-8187-ee68b222517d"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("42b17ef7-36cf-4a45-b1b7-5595796565cc"), 0 },
                    { new Guid("b4b08fb2-b436-4b3a-8fdb-b67b61926a3b"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("9c041be6-0049-4060-8c3d-0a1711dff500"), 0 },
                    { new Guid("c0b6c1fd-15df-4868-8072-ef54d0a925ca"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("b2430105-0289-45fc-95fc-7115c14393ed"), 0 },
                    { new Guid("c4414ced-ef84-44e6-a222-dd3114041a1d"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("10279fe5-a3dd-4770-afb6-82a4f350cea5"), 0 },
                    { new Guid("d12ff279-0ccb-49f2-a398-96ad6702a1f5"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("27fbed5f-b6de-4b93-9a4d-6eaae598d157"), 0 },
                    { new Guid("e2e08cef-f9bc-43a0-b178-3634a3cf0548"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("33231a13-5152-4ead-b34f-7c903d4bac54"), 0 },
                    { new Guid("ef0a462a-6858-4c47-920c-0ea86b81c542"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("0e041e67-51c3-4d6e-a7b8-3684c5a9a793"), 0 },
                    { new Guid("fdfb81d8-03c8-4d28-9df0-432a11981383"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("b800a56e-863d-4cfc-997b-5ecad5c7c92b"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3849a90a-f2e0-485a-a0e2-bc239e6d3e66"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("4aef32ba-570e-449d-b1fc-14f18c2823aa") },
                    { new Guid("80a923ca-3a79-4187-8b4c-c85188961d97"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("83063991-18b4-431b-9944-39e1d41c104d") },
                    { new Guid("8f3434e3-8952-44ee-8864-ee697f345122"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("886eef82-fe91-4483-a32c-8b10f0a569be") },
                    { new Guid("a92b4470-4f9e-47be-b5f5-37aa25492db0"), new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"), new Guid("7b5db418-8974-4dae-9d39-c44aa4bc08c3") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("fac944a2-36cd-43ff-9c21-8c63a90e6160"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("0cc3cca0-0921-4db7-adea-fe9239a93f24"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("1405a4e3-2a27-4c95-b2e7-b6c308f122b3"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("19ad4e4c-066e-4c80-b826-c9508e94c295"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("452219fe-a33f-4324-ba70-68fc7c07f948"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("588a0cf6-5ca6-4d54-8aec-1d49ed748e2d"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("878e9d11-48be-4759-8762-e6a71a9eee0d"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("986f4b4e-63c7-4c8d-b301-524463eade8e"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("9d6c13e4-9c37-4462-b155-34fc40e1727d"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("a95d0af4-6a9b-4504-8187-ee68b222517d"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("b4b08fb2-b436-4b3a-8fdb-b67b61926a3b"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("c0b6c1fd-15df-4868-8072-ef54d0a925ca"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("c4414ced-ef84-44e6-a222-dd3114041a1d"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("d12ff279-0ccb-49f2-a398-96ad6702a1f5"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("e2e08cef-f9bc-43a0-b178-3634a3cf0548"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("ef0a462a-6858-4c47-920c-0ea86b81c542"));

            migrationBuilder.DeleteData(
                table: "tblPieceGame",
                keyColumn: "Id",
                keyValue: new Guid("fdfb81d8-03c8-4d28-9df0-432a11981383"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("29a8209c-bc1e-45fb-975a-b3be403c8eb7"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("3849a90a-f2e0-485a-a0e2-bc239e6d3e66"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("80a923ca-3a79-4187-8b4c-c85188961d97"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("8f3434e3-8952-44ee-8864-ee697f345122"));

            migrationBuilder.DeleteData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: new Guid("a92b4470-4f9e-47be-b5f5-37aa25492db0"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("c225c4f3-f378-467b-9722-7c5852cb584e"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("04605c78-c0f0-4713-869d-96235dadea16"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("0e041e67-51c3-4d6e-a7b8-3684c5a9a793"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("10279fe5-a3dd-4770-afb6-82a4f350cea5"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("1f3f10b6-9b51-4e53-b478-561a0f55fefc"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("27fbed5f-b6de-4b93-9a4d-6eaae598d157"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("33231a13-5152-4ead-b34f-7c903d4bac54"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("42b17ef7-36cf-4a45-b1b7-5595796565cc"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("5fe75ac6-a73e-432c-9c17-6095af2d4aef"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("6c2adcf3-7546-459d-a231-bcd0796624bb"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("904bc09b-d190-4745-a667-d2eac333bcfe"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("9c041be6-0049-4060-8c3d-0a1711dff500"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("b2430105-0289-45fc-95fc-7115c14393ed"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("b800a56e-863d-4cfc-997b-5ecad5c7c92b"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("d21b8cd5-ec32-4893-96e4-199312480fa6"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("d556e30c-6d87-48c3-97c9-3ae6335a2cb0"));

            migrationBuilder.DeleteData(
                table: "tblPiece",
                keyColumn: "Id",
                keyValue: new Guid("f479f108-fffb-472a-80b2-3d4b00b7b4d0"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("4aef32ba-570e-449d-b1fc-14f18c2823aa"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("7b5db418-8974-4dae-9d39-c44aa4bc08c3"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("83063991-18b4-431b-9944-39e1d41c104d"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("886eef82-fe91-4483-a32c-8b10f0a569be"));

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
        }
    }
}
