using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class testestasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentStatusLog_Statuses_StatusId",
                table: "ShipmentStatusLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 8, 13, 31, 28, 299, DateTimeKind.Local).AddTicks(1016));

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentStatusLog_Status_StatusId",
                table: "ShipmentStatusLog",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentStatusLog_Status_StatusId",
                table: "ShipmentStatusLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 8, 13, 27, 57, 264, DateTimeKind.Local).AddTicks(1776));

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentStatusLog_Statuses_StatusId",
                table: "ShipmentStatusLog",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
