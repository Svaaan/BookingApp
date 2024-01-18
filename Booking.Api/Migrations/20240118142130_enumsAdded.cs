using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class enumsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shows_salon_SalonID",
                table: "shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_salon",
                table: "salon");

            migrationBuilder.RenameTable(
                name: "salon",
                newName: "salons");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "salons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_salons",
                table: "salons",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "salons",
                keyColumn: "ID",
                keyValue: 1,
                column: "Status",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_shows_salons_SalonID",
                table: "shows",
                column: "SalonID",
                principalTable: "salons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shows_salons_SalonID",
                table: "shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_salons",
                table: "salons");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "salons");

            migrationBuilder.RenameTable(
                name: "salons",
                newName: "salon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_salon",
                table: "salon",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_shows_salon_SalonID",
                table: "shows",
                column: "SalonID",
                principalTable: "salon",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
