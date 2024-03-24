using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class Databaseconfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_reservations_ShowId",
                table: "reservations",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_shows_ShowId",
                table: "reservations",
                column: "ShowId",
                principalTable: "shows",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_shows_ShowId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_ShowId",
                table: "reservations");
        }
    }
}
