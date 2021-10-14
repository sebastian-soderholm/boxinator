using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class boxtest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Box",
                columns: new[] { "Id", "BoxTypeId", "Color", "ShipmentId" },
                values: new object[] { 2, 1, "(123,765,3)", 1 });

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 15, 1, 4, 2, 798, DateTimeKind.Local).AddTicks(6325));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 15, 1, 4, 2, 801, DateTimeKind.Local).AddTicks(3959));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Box",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 15, 1, 1, 42, 383, DateTimeKind.Local).AddTicks(5481));

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 10, 15, 1, 1, 42, 386, DateTimeKind.Local).AddTicks(5343));
        }
    }
}
