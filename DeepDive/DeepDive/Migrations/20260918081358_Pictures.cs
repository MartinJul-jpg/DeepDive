using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeepDive.Migrations
{
    /// <inheritdoc />
    public partial class Pictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
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
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Start = table.Column<DateOnly>(type: "date", nullable: false),
                    End = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "1ac343b0-10ef-1a1c-1f64-18daaca77b5b", "Admin", "ADMIN" },
                    { "2", "2ac343b0-20ef-2a1c-2f64-28daaca77b5b", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "84cd148c-e055-4d01-8a32-d06e7489e2a3", 0, "451190ea-b605-4320-980a-6b863f169b73", "kaede@mail.weeb", false, true, null, "KAEDE@MAIL.WEEB", "KAEDE@MAIL.WEEB", "AQAAAAIAAYagAAAAEPLm8PWZaT7SCiLonPE0fCuRfv1/I5tpvitirqwxafypHXTXHtZXEe2fwvsW26UyPQ==", null, false, "E6PKSVRQKLTX6JM23QOOG7NSEMZAT47O", false, "kaede@mail.weeb" },
                    { "9542f2d5-1f05-405c-b9df-eead849cd99f", 0, "e322824d-2dab-48bc-ade6-9fc4d2a873bc", "admin@mail.weeb", false, true, null, "ADMIN@MAIL.WEEB", "ADMIN@MAIL.WEEB", "AQAAAAIAAYagAAAAEBEVmLtgwFMZs35MUjXhKmcugpijRN4PRdczdzESGAEHJ3JA9IPKoq6XCexeD3KMtg==", null, false, "OINHVGDBRL3BYMS5GX6ZUR6LIH6YPQNN", false, "admin@mail.weeb" }
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
                columns: new[] { "ProductId", "Amount", "Brand", "ImgPath", "BCD_Model", "Price", "BCD_Size", "product_type" },
                values: new object[,]
                {
                    { 10, 5, "Scubapro", "~/lib/custom/resource/ScubaproNavigatorLiteBCD.jpg", "Navigator Lite BCD", 125.0, "S,M,L", "product_BCD" },
                    { 11, 5, "Scubapro", "~/lib/custom/resource/ScubaproBCDGlide.jpg", "BCD Glide", 140.0, "S,M,L", "product_BCD" },
                    { 12, 5, "Scubapro", "~/lib/custom/resource/ScubaproBCDHydrosPro.jpg", "BCD Hydros Pro", 200.0, "S,M,L", "product_BCD" },
                    { 13, 5, "Seac", "~/lib/custom/resource/SeacBCDModular.jpg", "BCD Modular", 125.0, "S,M,L", "product_BCD" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "ImgPath", "Price", "Volume", "product_type" },
                values: new object[,]
                {
                    { 20, 5, "ScubaPro", "~/lib/custom/resource/ScubaproDykkerflaske5liter.jpg", 150.0, "5 liter", "product_Tank" },
                    { 21, 5, "ScubaPro", "~/lib/custom/resource/ScubaproDykkerflaske10liter.jpg", 160.0, "10 liter", "product_Tank" },
                    { 22, 5, "ScubaPro", "~/lib/custom/resource/ScubaproDykkerflaske12liter.jpg", 170.0, "12 liter", "product_Tank" },
                    { 23, 5, "ScubaPro", "~/lib/custom/resource/ScubaproDykkerflaske15liter.jpg", 180.0, "15 liter", "product_Tank" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "FirstStage", "ImgPath", "Octopus", "Price", "SecondStage", "product_type" },
                values: new object[,]
                {
                    { 30, 5, "ScubaPro", "MK25EVO", "~/lib/custom/resource/scubapro_mk25_s600_regulator_2_98f86760b6.jpg", "R105", 125.0, "S600", "product_RegulatorSet" },
                    { 31, 5, "ScubaPro", "MK17EVO", "~/lib/custom/resource/scubapro_mk25_s600_regulator_2_98f86760b6.jpg", "R095", 100.0, "C370", "product_RegulatorSet" },
                    { 32, 5, "ScubaPro", "MK25EVO BT", "~/lib/custom/resource/mk25-bt-a700carbon-s270.w1200.jpg", "S270", 150.0, "A700 Carbon BT", "product_RegulatorSet" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "ImgPath", "Model", "Price", "Size", "product_type" },
                values: new object[,]
                {
                    { 40, 5, "ScubaPro", "~/lib/custom/resource/ScubaproJetFin.jpg", "Jet Fin", 50.0, "XS,S,M,L,XL", "product_Fin" },
                    { 41, 5, "ScubaPro", "~/lib/custom/resource/ScubaproGoTravel.jpg", "GO Travel", 50.0, "XS,S,M,L,XL", "product_Fin" },
                    { 42, 5, "ScubaPro", "~/lib/custom/resource/ScubaproSeawingSupernova.jpg", "Seawing Supernova", 60.0, "XS,S,M,L,XL", "product_Fin" },
                    { 43, 5, "Seac", "~/lib/custom/resource/SeacPropulsion.jpg", "Propulsion", 50.0, "XS,S,M,L,XL", "product_Fin" },
                    { 44, 5, "Seac", "~/lib/custom/resource/SeacALA.jpg", "ALA", 50.0, "XS,S,M,L,XL", "product_Fin" },
                    { 45, 5, "Fourth Element", "~/lib/custom/resource/FourthElementTech.jpg", "Tech", 75.0, "XS,S,M,L,XL", "product_Fin" },
                    { 46, 5, "Fourth Element", "~/lib/custom/resource/FourthElementRecFin.jpg", "Rec Fin", 80.0, "XS,S,M,L,XL", "product_Fin" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "Gender", "ImgPath", "DivingSuit_Model", "Price", "DivingSuit_Size", "Thickness", "Type", "product_type" },
                values: new object[,]
                {
                    { 50, 5, "Scubapro", "Herre/Dame", "~/lib/custom/resource/ScubaproDefinition3mm.jpg", "Definition", 100.0, "XS,S,N,L,XL", "3 mm", "Våddragt", "product_DivingSuit" },
                    { 51, 5, "Scubapro", "Herre/Dame", "~/lib/custom/resource/ScubaproDefinition5mm.jpg", "Definition", 100.0, "XS,S,N,L,XL", "5 mm", "Våddragt", "product_DivingSuit" },
                    { 52, 5, "Scubapro", "Herre/Dame", "~/lib/custom/resource/ScubaproDefinition7mm.jpg", "Definition", 100.0, "XS,S,N,L,XL", "7 mm", "Våddragt", "product_DivingSuit" },
                    { 53, 5, "Waterproof", "Herre/Dame", "~/lib/custom/resource/WaterproofW5.jpg", "W5", 100.0, "XS,S,N,L,XL", "3.5 mm", "Våddragt", "product_DivingSuit" },
                    { 54, 5, "Fourth Element", "Herre/Dame", "~/lib/custom/resource/FourthElementProteus.jpg", "Proteus", 120.0, "XS,S,N,L,XL", "5 mm", "Våddragt", "product_DivingSuit" },
                    { 55, 5, "Scubapro", "Herre/Dame", "~/lib/custom/resource/ScubaproExodry4-0.jpg", "Exodry 4.0", 300.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DivingSuit" },
                    { 56, 5, "Waterproof", "Herre/Dame", "~/lib/custom/resource/WaterproofD7Evo.jpg", "D7 Evo", 320.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DivingSuit" },
                    { 57, 5, "Santi", "Herre/Dame", "~/lib/custom/resource/SantiE-LitePlus.jpg", "E.Lite plus", 350.0, "XS,S,N,L,XL", " ", "Tørdragt", "product_DivingSuit" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Amount", "Brand", "ImgPath", "Snorkel_Model", "Price", "product_type" },
                values: new object[,]
                {
                    { 60, 5, "Scubapro", "~/lib/custom/resource/ScubaproGhost.jpg", "Ghost", 50.0, "product_Snorkel" },
                    { 61, 5, "Scubapro", "~/lib/custom/resource/ScubaproD-Mask.jpg", "D-Mask", 60.0, "product_Snorkel" },
                    { 62, 5, "Scubapro", "~/lib/custom/resource/ScubaproSpectraMini.jpg", "Spectra Mini", 50.0, "product_Snorkel" },
                    { 63, 5, "Scubapro", "~/lib/custom/resource/ScubaproCrystalVU.jpg", "Crystal VU", 75.0, "product_Snorkel" },
                    { 64, 5, "Fourth Element", "~/lib/custom/resource/FourthElementScoutKontrast.jpg", "Scout Kontrast", 75.0, "product_Snorkel" },
                    { 65, 5, "Fourth Element", "~/lib/custom/resource/FourthElementScoutEnhance.jpg", "Scout Enhance", 75.0, "product_Snorkel" },
                    { 66, 5, "Tusa", "~/lib/custom/resource/TusaElement.jpg", "Element", 75.0, "product_Snorkel" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "84cd148c-e055-4d01-8a32-d06e7489e2a3" },
                    { "1", "9542f2d5-1f05-405c-b9df-eead849cd99f" },
                    { "2", "9542f2d5-1f05-405c-b9df-eead849cd99f" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "End", "Start", "UserId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 9, 20), new DateOnly(2026, 9, 18), "84cd148c-e055-4d01-8a32-d06e7489e2a3" },
                    { 2, new DateOnly(2026, 9, 20), new DateOnly(2026, 9, 18), "84cd148c-e055-4d01-8a32-d06e7489e2a3" }
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Package_Products");

            migrationBuilder.DropTable(
                name: "Product_Bookings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
