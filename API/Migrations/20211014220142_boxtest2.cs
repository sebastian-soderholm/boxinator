using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class boxtest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 15, 1, 1, 42, 383, DateTimeKind.Local).AddTicks(5481));

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "RECIEVED" },
                    { 3, "INTRANSIT" },
                    { 4, "COMPLETED" },
                    { 5, "CANCELLED" }
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatusLog",
                columns: new[] { "Id", "Date", "ShipmentId", "StatusId" },
                values: new object[] { 2, new DateTime(2021, 10, 15, 1, 1, 42, 386, DateTimeKind.Local).AddTicks(5343), 1, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 15, 0, 59, 11, 367, DateTimeKind.Local).AddTicks(422));
        }
    }
}
