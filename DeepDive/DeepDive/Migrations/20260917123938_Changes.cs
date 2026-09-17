using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeepDive.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 9, 19), new DateOnly(2026, 9, 17) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 9, 19), new DateOnly(2026, 9, 17) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 9, 16), new DateOnly(2026, 9, 14) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateOnly(2026, 9, 16), new DateOnly(2026, 9, 14) });
        }
    }
}
