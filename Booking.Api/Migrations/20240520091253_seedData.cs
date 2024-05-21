using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "bookers",
                columns: new[] { "Id", "BookingNumber", "Email", "LastName", "Name", "PhoneNumber" },
                values: new object[] { 1, "7890", null, "Doe", "John", "1234567890" });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "4zY3HBkRejtIqJtw2KbBay6t6G4X8mEbgzgjN60SV3c=", new byte[] { 239, 113, 105, 234, 234, 234, 156, 163, 67, 91, 148, 112, 164, 234, 138, 22, 232, 16, 66, 19, 226, 211, 255, 226, 8, 83, 143, 2, 152, 106, 229, 91 } });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "4zY3HBkRejtIqJtw2KbBay6t6G4X8mEbgzgjN60SV3c=", new byte[] { 239, 113, 105, 234, 234, 234, 156, 163, 67, 91, 148, 112, 164, 234, 138, 22, 232, 16, 66, 19, 226, 211, 255, 226, 8, 83, 143, 2, 152, 106, 229, 91 } });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "4zY3HBkRejtIqJtw2KbBay6t6G4X8mEbgzgjN60SV3c=", new byte[] { 239, 113, 105, 234, 234, 234, 156, 163, 67, 91, 148, 112, 164, 234, 138, 22, 232, 16, 66, 19, 226, 211, 255, 226, 8, 83, 143, 2, 152, 106, 229, 91 } });

            migrationBuilder.InsertData(
                table: "shows",
                columns: new[] { "Id", "AvailableSeats", "EndTime", "MovieId", "PricePerSeat", "SalonId", "StartTime", "VAT" },
                values: new object[] { 1, 15, new DateTime(2024, 6, 4, 9, 12, 53, 234, DateTimeKind.Utc).AddTicks(6949), 2, 120m, 1, new DateTime(2024, 5, 20, 9, 12, 53, 234, DateTimeKind.Utc).AddTicks(6948), 25m });

            migrationBuilder.InsertData(
                table: "reservations",
                columns: new[] { "Id", "BookedSeats", "BookerId", "ReservationTime", "ShowId" },
                values: new object[] { 1, 2, 1, new DateTime(2024, 5, 20, 14, 12, 53, 234, DateTimeKind.Utc).AddTicks(6964), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "bookers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "shows",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "S5twTrT7VgtSFwAQU459D52B145g53a0EIm2Vd06+e0=", new byte[] { 57, 255, 46, 38, 222, 94, 40, 41, 235, 5, 61, 68, 151, 112, 196, 250, 81, 40, 108, 243, 117, 23, 140, 143, 110, 190, 94, 128, 57, 190, 120, 133 } });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "S5twTrT7VgtSFwAQU459D52B145g53a0EIm2Vd06+e0=", new byte[] { 57, 255, 46, 38, 222, 94, 40, 41, 235, 5, 61, 68, 151, 112, 196, 250, 81, 40, 108, 243, 117, 23, 140, 143, 110, 190, 94, 128, 57, 190, 120, 133 } });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "S5twTrT7VgtSFwAQU459D52B145g53a0EIm2Vd06+e0=", new byte[] { 57, 255, 46, 38, 222, 94, 40, 41, 235, 5, 61, 68, 151, 112, 196, 250, 81, 40, 108, 243, 117, 23, 140, 143, 110, 190, 94, 128, 57, 190, 120, 133 } });
        }
    }
}
