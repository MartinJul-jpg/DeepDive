using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeepDive.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    BookedProducts = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    PackageId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Products_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId");
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
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Bookings", x => new { x.ProductId, x.BookingId });
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
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "BCD_Model", "PackageId", "Price", "BCD_Size", "product_type" },
                values: new object[,]
                {
                    { 10, 1, "Scubapro", "Navigator Lite BCD", null, 125.0, "S,M,L", "product_BCD" },
                    { 11, 1, "Scubapro", "BCD Glide", null, 140.0, "S,M,L", "product_BCD" },
                    { 12, 1, "Scubapro", "BCD Hydros Pro", null, 200.0, "S,M,L", "product_BCD" },
                    { 13, 1, "Seac", "BCD Modular", null, 125.0, "S,M,L", "product_BCD" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "PackageId", "Price", "Volume", "product_type" },
                values: new object[,]
                {
                    { 20, 1, "ScubaPro", null, 150.0, "5 liter", "product_T" },
                    { 21, 1, "ScubaPro", null, 160.0, "10 liter", "product_T" },
                    { 22, 1, "ScubaPro", null, 170.0, "12 liter", "product_T" },
                    { 23, 1, "ScubaPro", null, 180.0, "15 liter", "product_T" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "FirstStage", "Octopus", "PackageId", "Price", "SecondStage", "product_type" },
                values: new object[,]
                {
                    { 30, 1, "ScubaPro", "MK25EVO", "R105", null, 125.0, "S600", "product_RS" },
                    { 31, 1, "ScubaPro", "MK17EVO", "R095", null, 100.0, "C370", "product_RS" },
                    { 32, 1, "ScubaPro", "MK25EVO BT", "S270", null, 150.0, "A700 Carbon BT", "product_RS" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Model", "PackageId", "Price", "Size", "product_type" },
                values: new object[,]
                {
                    { 40, 1, "ScubaPro", "Jet Fin", null, 50.0, "XS,S,M,L,XL", "product_F" },
                    { 41, 1, "ScubaPro", "GO Travel", null, 50.0, "XS,S,M,L,XL", "product_F" },
                    { 42, 1, "ScubaPro", "Seawing Supernova", null, 60.0, "XS,S,M,L,XL", "product_F" },
                    { 43, 1, "Seac", "Propulsion", null, 50.0, "XS,S,M,L,XL", "product_F" },
                    { 44, 1, "Seac", "ALA", null, 50.0, "XS,S,M,L,XL", "product_F" },
                    { 45, 1, "Fourth Element", "Tech", null, 75.0, "XS,S,M,L,XL", "product_F" },
                    { 46, 1, "Fourth Element", "Rec Fin", null, 80.0, "XS,S,M,L,XL", "product_F" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Gender", "DivingSuit_Model", "PackageId", "Price", "DivingSuit_Size", "Thickness", "Type", "product_type" },
                values: new object[,]
                {
                    { 50, 1, "Scubapro", "Herre/Dame", "Definition", null, 100.0, "XS,S,N,L,XL", "3 mm", "Våddragt", "product_DS" },
                    { 51, 1, "Scubapro", "Herre/Dame", "Definition", null, 100.0, "XS,S,N,L,XL", "5 mm", "Våddragt", "product_DS" },
                    { 52, 1, "Scubapro", "Herre/Dame", "Definition", null, 100.0, "XS,S,N,L,XL", "7 mm", "Våddragt", "product_DS" },
                    { 53, 1, "Waterproof", "Herre/Dame", "W5", null, 100.0, "XS,S,N,L,XL", "3.5 mm", "Våddragt", "product_DS" },
                    { 54, 1, "Fourth Element", "Herre/Dame", "Proteus", null, 120.0, "XS,S,N,L,XL", "5 mm", "Våddragt", "product_DS" },
                    { 55, 1, "Scubapro", "Herre/Dame", "Exodry 4.0", null, 300.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DS" },
                    { 56, 1, "Waterproof", "Herre/Dame", "D7 Evo", null, 320.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DS" },
                    { 57, 1, "Santi", "Herre/Dame", "E.Lite plus", null, 350.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DS" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Snorkel_Model", "PackageId", "Price", "product_type" },
                values: new object[,]
                {
                    { 60, 1, "Scubapro", "Ghost", null, 50.0, "product_SNO" },
                    { 61, 1, "Scubapro", "D-Mask", null, 60.0, "product_SNO" },
                    { 62, 1, "Scubapro", "Spectra Mini", null, 50.0, "product_SNO" },
                    { 63, 1, "Scubapro", "Crystal VU", null, 75.0, "product_SNO" },
                    { 64, 1, "Fourth Element", "Scout Kontrast", null, 75.0, "product_SNO" },
                    { 65, 1, "Fourth Element", "Scout Enhance", null, 75.0, "product_SNO" },
                    { 66, 1, "Tusa", "Element", null, 75.0, "product_SNO" }
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
                name: "IX_Products_PackageId",
                table: "Products",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Package_Products");

            migrationBuilder.DropTable(
                name: "Product_Bookings");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
