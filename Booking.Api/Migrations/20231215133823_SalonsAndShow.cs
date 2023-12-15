using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class SalonsAndShow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "salon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "shows",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    SalonID = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_shows_movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shows_salon_SalonID",
                        column: x => x.SalonID,
                        principalTable: "salon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "salon",
                columns: new[] { "ID", "Name", "NumberOfSeats" },
                values: new object[] { 1, "Salon 1", 30 });

            migrationBuilder.CreateIndex(
                name: "IX_shows_MovieID",
                table: "shows",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_shows_SalonID",
                table: "shows",
                column: "SalonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shows");

            migrationBuilder.DropTable(
                name: "salon");
        }
    }
}
