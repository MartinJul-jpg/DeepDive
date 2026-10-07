using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeepDive.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 10, 9), new DateOnly(2026, 10, 7) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 10, 9), new DateOnly(2026, 10, 7) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 10, 3), new DateOnly(2026, 10, 1) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 10, 3), new DateOnly(2026, 10, 1) });
        }
    }
}
