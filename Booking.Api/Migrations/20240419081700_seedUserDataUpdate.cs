using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class seedUserDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "Richard@example.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "tess@example.com");
        }
    }
}
