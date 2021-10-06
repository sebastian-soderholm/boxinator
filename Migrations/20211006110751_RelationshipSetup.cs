using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class RelationshipSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_Box_ShipmentId",
                table: "Box");

            migrationBuilder.CreateTable(
                name: "ShipmentStatusLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentStatusLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentStatusLog_Shipment_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatusLog",
                columns: new[] { "Id", "Date", "ShipmentId", "Status" },
                values: new object[] { 1, new DateTime(2021, 10, 6, 14, 7, 50, 646, DateTimeKind.Local).AddTicks(2550), 1, "CREATED" });

            migrationBuilder.CreateIndex(
                name: "IX_Box_ShipmentId",
                table: "Box",
                column: "ShipmentId",
                unique: true,
                filter: "[ShipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatusLog_ShipmentId",
                table: "ShipmentStatusLog",
                column: "ShipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentStatusLog");

            migrationBuilder.DropIndex(
                name: "IX_Box_ShipmentId",
                table: "Box");

            migrationBuilder.CreateTable(
                name: "ShipmentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentStatus_Shipment_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatus",
                columns: new[] { "Id", "Date", "ShipmentId", "Status" },
                values: new object[] { 1, new DateTime(2021, 10, 5, 14, 49, 45, 326, DateTimeKind.Local).AddTicks(6498), 1, "CREATED" });

            migrationBuilder.CreateIndex(
                name: "IX_Box_ShipmentId",
                table: "Box",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatus_ShipmentId",
                table: "ShipmentStatus",
                column: "ShipmentId");
        }
    }
}
