using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryMultiplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipment_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Box",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BoxTypeId = table.Column<int>(type: "int", nullable: false),
                    ShipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Box_BoxType_BoxTypeId",
                        column: x => x.BoxTypeId,
                        principalTable: "BoxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Box_Shipment_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentStatusLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ShipmentStatusLog_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BoxType",
                columns: new[] { "Id", "Name", "Weight" },
                values: new object[] { 1, "Premium", 8.0 });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "CREATED" });

            migrationBuilder.InsertData(
                table: "Zone",
                columns: new[] { "Id", "CountryMultiplier", "Name" },
                values: new object[] { 1, 100, "Europe" });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name", "ZoneId" },
                values: new object[] { 1, "Finland", 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountType", "CountryId", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber", "ZipCode" },
                values: new object[] { 1, "REGISTERED_USER", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "awesomemartta@gs.com", "Martta", "Johnsson", "16064650210", "610650" });

            migrationBuilder.InsertData(
                table: "Shipment",
                columns: new[] { "Id", "Cost", "CountryId", "ReceiverName", "UserId" },
                values: new object[] { 1, 100.0, 1, "Petteri Smith", 1 });

            migrationBuilder.InsertData(
                table: "Box",
                columns: new[] { "Id", "BoxTypeId", "Color", "ShipmentId" },
                values: new object[] { 1, 1, "(32,178,170)", 1 });

            migrationBuilder.InsertData(
                table: "ShipmentStatusLog",
                columns: new[] { "Id", "Date", "ShipmentId", "StatusId" },
                values: new object[] { 1, new DateTime(2021, 10, 8, 14, 44, 42, 684, DateTimeKind.Local).AddTicks(3646), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Box_BoxTypeId",
                table: "Box",
                column: "BoxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Box_ShipmentId",
                table: "Box",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ZoneId",
                table: "Country",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_CountryId",
                table: "Shipment",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_UserId",
                table: "Shipment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatusLog_ShipmentId",
                table: "ShipmentStatusLog",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatusLog_StatusId",
                table: "ShipmentStatusLog",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CountryId",
                table: "User",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Box");

            migrationBuilder.DropTable(
                name: "ShipmentStatusLog");

            migrationBuilder.DropTable(
                name: "BoxType");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Zone");
        }
    }
}
