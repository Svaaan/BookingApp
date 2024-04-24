using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class addresss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "movieTheatres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookingNumber",
                table: "bookers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "company",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Adress", "Country" },
                values: new object[] { "Testgatan 123b", "Sverige" });

            migrationBuilder.UpdateData(
                table: "movieTheatres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Adress",
                value: "Biogatan 12a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "movieTheatres");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "company");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "company");

            migrationBuilder.DropColumn(
                name: "BookingNumber",
                table: "bookers");
        }
    }
}
