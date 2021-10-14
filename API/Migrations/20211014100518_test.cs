using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiverName",
                table: "Shipment",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Shipment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Shipment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Shipment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "FirstName", "LastName", "ZipCode" },
                values: new object[] { "Keskuskatu 1", "Petteri", "Smith", "00100" });

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 14, 13, 5, 17, 877, DateTimeKind.Local).AddTicks(6010));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Shipment");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Shipment",
                newName: "ReceiverName");

            migrationBuilder.UpdateData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReceiverName",
                value: "Petteri Smith");

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 13, 15, 42, 45, 869, DateTimeKind.Local).AddTicks(3828));
        }
    }
}
