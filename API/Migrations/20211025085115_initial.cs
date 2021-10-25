using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace boxinator.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
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
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
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
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                values: new object[,]
                {
                    { 1, "Premium", 8 },
                    { 2, "Deluxe", 5 },
                    { 3, "Humble", 2 },
                    { 4, "Basic", 1 }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CREATED" },
                    { 2, "RECIEVED" },
                    { 3, "INTRANSIT" },
                    { 4, "COMPLETED" },
                    { 5, "CANCELLED" }
                });

            migrationBuilder.InsertData(
                table: "Zone",
                columns: new[] { "Id", "CountryMultiplier", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Source Zone" },
                    { 2, 10, "Europe" },
                    { 3, 20, "Asia, Australia & Oceania" },
                    { 4, 30, "Americas" },
                    { 5, 40, "Africa" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name", "ZoneId" },
                values: new object[,]
                {
                    { 2, "Sweden", 1 },
                    { 3, "Norway", 1 },
                    { 1, "Finland", 2 },
                    { 5, "Germany", 2 },
                    { 6, "China", 3 },
                    { 7, "Australia", 3 },
                    { 4, "USA", 4 },
                    { 9, "Brazil", 4 },
                    { 8, "Egypt", 5 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountType", "Address", "CountryId", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber", "ZipCode" },
                values: new object[,]
                {
                    { 1, "REGISTERED_USER", "Kungsgatan 54", 2, null, "awesomemartta@gmail.com", "Martta", "Johnsson", "16064650210", "11122" },
                    { 3, "ADMINISTRATOR", "Kaivokatu 10", 1, null, "developer.musetech@gmail.com", "Sebastian", "Söderholm", "0400959078", "06100" },
                    { 2, "ADMINISTRATOR", "Pirjontie 10", 9, null, "peppi.makela@gmail.com", "Peppi", "Mäkelä", "0504055679", "00200" },
                    { 4, "ADMINISTRATOR", "Tie 10", 8, null, "XXX", "Jani", "Vihervuori", "040123456", "06830" }
                });

            migrationBuilder.InsertData(
                table: "Shipment",
                columns: new[] { "Id", "Address", "Cost", "CountryId", "FirstName", "LastName", "UserId", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Keskuskatu 1", 100.0, 1, "Petteri", "Smith", 1, "00100" },
                    { 2, "Kungsgatan 54", 200.0, 2, "Martta", "Johnsson", 1, "11122" },
                    { 7, "Pirjontie 10", 987.0, 9, "Peppi", "Mäkelä", 3, "11122" },
                    { 10, "Kaivokatu 10", 824.0, 1, "Sebastian", "Söderholm", 3, "06100" },
                    { 12, "Keskuskatu 1", 51.0, 1, "Petteri", "Smith", 3, "00100" },
                    { 13, "Kungsgatan 54", 364.0, 2, "Martta", "Johnsson", 3, "11122" },
                    { 14, "Kungsgatan 54", 645.0, 2, "Martta", "Johnsson", 3, "11122" },
                    { 3, "Kungsgatan 54", 150.0, 2, "Martta", "Johnsson", 2, "11122" },
                    { 4, "Pirjontie 10", 258.0, 9, "Peppi", "Mäkelä", 2, "11122" },
                    { 5, "Kaivokatu 10", 4000.0, 1, "Sebastian", "Söderholm", 2, "06100" },
                    { 6, "Pirjontie 10", 156.0, 9, "Peppi", "Mäkelä", 2, "11122" },
                    { 8, "Pirjontie 10", 248.0, 9, "Peppi", "Mäkelä", 4, "11122" },
                    { 9, "Kaivokatu 10", 4006.0, 1, "Sebastian", "Söderholm", 4, "06100" },
                    { 11, "Keskuskatu 1", 36.0, 1, "Petteri", "Smith", 4, "00100" }
                });

            migrationBuilder.InsertData(
                table: "Box",
                columns: new[] { "Id", "BoxTypeId", "Color", "ShipmentId" },
                values: new object[,]
                {
                    { 1, 1, "rgb(32,178,170)", 1 },
                    { 25, 2, "rgb(0, 0, 255)", 14 },
                    { 4, 3, "rgb(235, 223, 52)", 3 },
                    { 5, 4, "rgb(232, 52, 235)", 3 },
                    { 7, 2, "rgb(129, 163, 36)", 4 },
                    { 8, 3, "rgb(163, 36, 97)", 4 },
                    { 9, 3, "rgb(230, 197, 213)", 5 },
                    { 10, 1, "rgb(89, 76, 76)", 6 },
                    { 11, 1, "rgb(31, 98, 128)", 6 },
                    { 12, 3, "rgb(207, 198, 171)", 6 },
                    { 14, 2, "rgb(248, 252, 3)", 8 },
                    { 15, 2, "rgb(51, 0, 31)", 8 },
                    { 16, 4, "rgb(54, 62, 74)", 9 },
                    { 17, 3, "rgb(133, 133, 133)", 9 },
                    { 18, 1, "rgb(255, 186, 186)", 9 },
                    { 20, 2, "rgb(35, 87, 20)", 11 },
                    { 24, 1, "rgb(255, 0, 0)", 14 },
                    { 23, 4, "rgb(255, 0, 187)", 14 },
                    { 6, 4, "rgb(235, 201, 52)", 3 },
                    { 2, 1, "rgb(123,765,3)", 1 },
                    { 19, 2, "rgb(168, 76, 57)", 10 },
                    { 22, 4, "rgb(0, 10, 84)", 13 },
                    { 13, 4, "rgb(136, 3, 252)", 7 },
                    { 3, 2, "rgb(235, 64, 52)", 2 },
                    { 21, 3, "rgb(8, 46, 48)", 12 }
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatusLog",
                columns: new[] { "Id", "Date", "ShipmentId", "StatusId" },
                values: new object[,]
                {
                    { 29, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8961), 13, 4 },
                    { 16, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8923), 7, 1 },
                    { 14, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8917), 6, 1 },
                    { 15, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8920), 6, 2 },
                    { 4, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8887), 2, 2 },
                    { 17, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8926), 8, 1 },
                    { 13, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8914), 5, 4 },
                    { 18, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8929), 8, 2 },
                    { 2, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8842), 1, 2 },
                    { 1, new DateTime(2021, 10, 25, 11, 51, 14, 241, DateTimeKind.Local).AddTicks(1543), 1, 1 },
                    { 19, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8932), 9, 1 },
                    { 3, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8883), 2, 1 },
                    { 12, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8911), 5, 3 },
                    { 10, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8905), 5, 1 },
                    { 28, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8958), 13, 3 },
                    { 27, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8955), 13, 2 },
                    { 26, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8952), 13, 1 }
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatusLog",
                columns: new[] { "Id", "Date", "ShipmentId", "StatusId" },
                values: new object[,]
                {
                    { 30, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8964), 14, 1 },
                    { 31, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8967), 14, 2 },
                    { 32, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8970), 14, 5 },
                    { 25, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8949), 12, 1 },
                    { 23, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8944), 11, 1 },
                    { 5, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8890), 3, 1 },
                    { 6, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8893), 3, 2 },
                    { 7, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8896), 3, 3 },
                    { 22, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8941), 10, 3 },
                    { 21, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8938), 10, 2 },
                    { 8, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8899), 4, 1 },
                    { 9, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8902), 4, 2 },
                    { 20, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8935), 10, 1 },
                    { 11, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8908), 5, 2 },
                    { 24, new DateTime(2021, 10, 25, 11, 51, 14, 244, DateTimeKind.Local).AddTicks(8947), 11, 5 }
                });

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
