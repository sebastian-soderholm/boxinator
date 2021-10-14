using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class fixedBoxesFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "BoxType",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BoxType",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BoxType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 14, 20, 13, 15, 80, DateTimeKind.Local).AddTicks(5206));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "BoxType",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BoxType",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "BoxType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 8.0);

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 14, 13, 5, 17, 877, DateTimeKind.Local).AddTicks(6010));
        }
    }
}
