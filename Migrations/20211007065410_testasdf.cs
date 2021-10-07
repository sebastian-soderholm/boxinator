using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class testasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Box_Shipment_ShipmentId",
                table: "Box");

            migrationBuilder.DropIndex(
                name: "IX_Box_ShipmentId",
                table: "Box");

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentId",
                table: "Box",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 7, 9, 54, 9, 921, DateTimeKind.Local).AddTicks(5204));

            migrationBuilder.CreateIndex(
                name: "IX_Box_ShipmentId",
                table: "Box",
                column: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Box_Shipment_ShipmentId",
                table: "Box",
                column: "ShipmentId",
                principalTable: "Shipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Box_Shipment_ShipmentId",
                table: "Box");

            migrationBuilder.DropIndex(
                name: "IX_Box_ShipmentId",
                table: "Box");

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentId",
                table: "Box",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "ShipmentStatusLog",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 10, 7, 9, 29, 1, 231, DateTimeKind.Local).AddTicks(5588));

            migrationBuilder.CreateIndex(
                name: "IX_Box_ShipmentId",
                table: "Box",
                column: "ShipmentId",
                unique: true,
                filter: "[ShipmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Box_Shipment_ShipmentId",
                table: "Box",
                column: "ShipmentId",
                principalTable: "Shipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
