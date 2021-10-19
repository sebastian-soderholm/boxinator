using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class adduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 19, 9, 40, 32, 718, DateTimeKind.Local).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 19, 9, 40, 32, 720, DateTimeKind.Local).AddTicks(8614));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 10, 19, 9, 40, 32, 720, DateTimeKind.Local).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2021, 10, 19, 9, 40, 32, 720, DateTimeKind.Local).AddTicks(8652));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2021, 10, 19, 9, 40, 32, 720, DateTimeKind.Local).AddTicks(8655));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CountryId",
                value: 2);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountType", "Address", "CountryId", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber", "ZipCode" },
                values: new object[] { 2, "ADMIN_USER", null, 1, null, "peppi.makela@gmail.com", "Peppi", "Mäkelä", "0504055679", "00200" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 18, 14, 1, 58, 698, DateTimeKind.Local).AddTicks(2647));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 18, 14, 1, 58, 701, DateTimeKind.Local).AddTicks(2122));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 10, 18, 14, 1, 58, 701, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2021, 10, 18, 14, 1, 58, 701, DateTimeKind.Local).AddTicks(2166));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2021, 10, 18, 14, 1, 58, 701, DateTimeKind.Local).AddTicks(2169));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CountryId",
                value: 1);
        }
    }
}
