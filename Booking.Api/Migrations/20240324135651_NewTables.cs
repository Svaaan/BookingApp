using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class NewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieTheatreId",
                table: "shows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieTheatreId",
                table: "movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "movieTheatres",
                columns: table => new
                {
                    MovieTheatreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movieTheatres", x => x.MovieTheatreId);
                    table.ForeignKey(
                        name: "FK_movieTheatres_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_users_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "movies",
                keyColumn: "ID",
                keyValue: 2,
                column: "MovieTheatreId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_shows_MovieTheatreId",
                table: "shows",
                column: "MovieTheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_movies_MovieTheatreId",
                table: "movies",
                column: "MovieTheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_movieTheatres_CompanyId",
                table: "movieTheatres",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_users_CompanyId",
                table: "users",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_movies_movieTheatres_MovieTheatreId",
                table: "movies",
                column: "MovieTheatreId",
                principalTable: "movieTheatres",
                principalColumn: "MovieTheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_shows_movieTheatres_MovieTheatreId",
                table: "shows",
                column: "MovieTheatreId",
                principalTable: "movieTheatres",
                principalColumn: "MovieTheatreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movies_movieTheatres_MovieTheatreId",
                table: "movies");

            migrationBuilder.DropForeignKey(
                name: "FK_shows_movieTheatres_MovieTheatreId",
                table: "shows");

            migrationBuilder.DropTable(
                name: "movieTheatres");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropIndex(
                name: "IX_shows_MovieTheatreId",
                table: "shows");

            migrationBuilder.DropIndex(
                name: "IX_movies_MovieTheatreId",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "MovieTheatreId",
                table: "shows");

            migrationBuilder.DropColumn(
                name: "MovieTheatreId",
                table: "movies");
        }
    }
}
