using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeepDive.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateOnly>(type: "date", nullable: false),
                    End = table.Column<DateOnly>(type: "date", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BCD_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BCD_Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivingSuit_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivingSuit_Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thickness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstStage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondStage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Octopus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Snorkel_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Package_Products",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package_Products", x => new { x.PackageId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Package_Products_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Package_Products_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Bookings",
                columns: table => new
                {
                    Product_BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Bookings", x => x.Product_BookingId);
                    table.ForeignKey(
                        name: "FK_Product_Bookings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Bookings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "DurationInDays", "End", "Start", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 2, new DateOnly(2026, 9, 11), new DateOnly(2026, 9, 9), 0.0 },
                    { 2, 2, new DateOnly(2026, 9, 11), new DateOnly(2026, 9, 9), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                column: "PackageId",
                values: new object[]
                {
                    10,
                    20
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "BCD_Model", "Price", "BCD_Size", "product_type" },
                values: new object[,]
                {
                    { 10, 1, "Scubapro", "Navigator Lite BCD", 125.0, "S,M,L", "product_BCD" },
                    { 11, 1, "Scubapro", "BCD Glide", 140.0, "S,M,L", "product_BCD" },
                    { 12, 1, "Scubapro", "BCD Hydros Pro", 200.0, "S,M,L", "product_BCD" },
                    { 13, 1, "Seac", "BCD Modular", 125.0, "S,M,L", "product_BCD" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Price", "Volume", "product_type" },
                values: new object[,]
                {
                    { 20, 1, "ScubaPro", 150.0, "5 liter", "product_T" },
                    { 21, 1, "ScubaPro", 160.0, "10 liter", "product_T" },
                    { 22, 1, "ScubaPro", 170.0, "12 liter", "product_T" },
                    { 23, 1, "ScubaPro", 180.0, "15 liter", "product_T" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "FirstStage", "Octopus", "Price", "SecondStage", "product_type" },
                values: new object[,]
                {
                    { 30, 1, "ScubaPro", "MK25EVO", "R105", 125.0, "S600", "product_RS" },
                    { 31, 1, "ScubaPro", "MK17EVO", "R095", 100.0, "C370", "product_RS" },
                    { 32, 1, "ScubaPro", "MK25EVO BT", "S270", 150.0, "A700 Carbon BT", "product_RS" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Model", "Price", "Size", "product_type" },
                values: new object[,]
                {
                    { 40, 1, "ScubaPro", "Jet Fin", 50.0, "XS,S,M,L,XL", "product_F" },
                    { 41, 1, "ScubaPro", "GO Travel", 50.0, "XS,S,M,L,XL", "product_F" },
                    { 42, 1, "ScubaPro", "Seawing Supernova", 60.0, "XS,S,M,L,XL", "product_F" },
                    { 43, 1, "Seac", "Propulsion", 50.0, "XS,S,M,L,XL", "product_F" },
                    { 44, 1, "Seac", "ALA", 50.0, "XS,S,M,L,XL", "product_F" },
                    { 45, 1, "Fourth Element", "Tech", 75.0, "XS,S,M,L,XL", "product_F" },
                    { 46, 1, "Fourth Element", "Rec Fin", 80.0, "XS,S,M,L,XL", "product_F" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Gender", "DivingSuit_Model", "Price", "DivingSuit_Size", "Thickness", "Type", "product_type" },
                values: new object[,]
                {
                    { 50, 1, "Scubapro", "Herre/Dame", "Definition", 100.0, "XS,S,N,L,XL", "3 mm", "Våddragt", "product_DS" },
                    { 51, 1, "Scubapro", "Herre/Dame", "Definition", 100.0, "XS,S,N,L,XL", "5 mm", "Våddragt", "product_DS" },
                    { 52, 1, "Scubapro", "Herre/Dame", "Definition", 100.0, "XS,S,N,L,XL", "7 mm", "Våddragt", "product_DS" },
                    { 53, 1, "Waterproof", "Herre/Dame", "W5", 100.0, "XS,S,N,L,XL", "3.5 mm", "Våddragt", "product_DS" },
                    { 54, 1, "Fourth Element", "Herre/Dame", "Proteus", 120.0, "XS,S,N,L,XL", "5 mm", "Våddragt", "product_DS" },
                    { 55, 1, "Scubapro", "Herre/Dame", "Exodry 4.0", 300.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DS" },
                    { 56, 1, "Waterproof", "Herre/Dame", "D7 Evo", 320.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DS" },
                    { 57, 1, "Santi", "Herre/Dame", "E.Lite plus", 350.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DS" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Snorkel_Model", "Price", "product_type" },
                values: new object[,]
                {
                    { 60, 1, "Scubapro", "Ghost", 50.0, "product_SNO" },
                    { 61, 1, "Scubapro", "D-Mask", 60.0, "product_SNO" },
                    { 62, 1, "Scubapro", "Spectra Mini", 50.0, "product_SNO" },
                    { 63, 1, "Scubapro", "Crystal VU", 75.0, "product_SNO" },
                    { 64, 1, "Fourth Element", "Scout Kontrast", 75.0, "product_SNO" },
                    { 65, 1, "Fourth Element", "Scout Enhance", 75.0, "product_SNO" },
                    { 66, 1, "Tusa", "Element", 75.0, "product_SNO" }
                });

            migrationBuilder.InsertData(
                table: "Package_Products",
                columns: new[] { "PackageId", "ProductId" },
                values: new object[,]
                {
                    { 10, 10 },
                    { 10, 20 },
                    { 10, 30 },
                    { 10, 40 },
                    { 10, 50 },
                    { 10, 60 },
                    { 20, 40 },
                    { 20, 60 }
                });

            migrationBuilder.InsertData(
                table: "Product_Bookings",
                columns: new[] { "Product_BookingId", "BookingId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 2, 1, 10 },
                    { 3, 1, 10 },
                    { 4, 2, 20 },
                    { 5, 2, 20 },
                    { 6, 2, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Package_Products_ProductId",
                table: "Package_Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Bookings_BookingId",
                table: "Product_Bookings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Bookings_ProductId",
                table: "Product_Bookings",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Package_Products");

            migrationBuilder.DropTable(
                name: "Product_Bookings");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
