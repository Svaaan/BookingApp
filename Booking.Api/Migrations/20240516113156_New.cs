using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employees_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "CompanyId", "Email", "LastName", "Name", "Password", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, 1, "john@example.com", "Doe", "John", "S5twTrT7VgtSFwAQU459D52B145g53a0EIm2Vd06+e0=", "Admin", new byte[] { 57, 255, 46, 38, 222, 94, 40, 41, 235, 5, 61, 68, 151, 112, 196, 250, 81, 40, 108, 243, 117, 23, 140, 143, 110, 190, 94, 128, 57, 190, 120, 133 } },
                    { 2, 1, "tess@example.com", "Doe", "Tess", "S5twTrT7VgtSFwAQU459D52B145g53a0EIm2Vd06+e0=", "Employee", new byte[] { 57, 255, 46, 38, 222, 94, 40, 41, 235, 5, 61, 68, 151, 112, 196, 250, 81, 40, 108, 243, 117, 23, 140, 143, 110, 190, 94, 128, 57, 190, 120, 133 } },
                    { 3, 1, "Richard@example.com", "Doe", "Richard", "S5twTrT7VgtSFwAQU459D52B145g53a0EIm2Vd06+e0=", "Manager", new byte[] { 57, 255, 46, 38, 222, 94, 40, 41, 235, 5, 61, 68, 151, 112, 196, 250, 81, 40, 108, 243, 117, 23, 140, 143, 110, 190, 94, 128, 57, 190, 120, 133 } }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_CompanyId",
                table: "employees",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CompanyId", "Email", "LastName", "Name", "Password", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, 1, "john@example.com", "Doe", "John", "F6gz1VzSO0OhkE1CsmiSDflgEU+82504dYwRrdAaS+8=", "Admin", new byte[] { 51, 132, 51, 23, 131, 253, 58, 134, 172, 192, 32, 186, 40, 171, 219, 83, 193, 244, 74, 67, 122, 225, 165, 154, 32, 90, 127, 229, 83, 37, 193, 116 } },
                    { 2, 1, "tess@example.com", "Doe", "Tess", "F6gz1VzSO0OhkE1CsmiSDflgEU+82504dYwRrdAaS+8=", "User", new byte[] { 51, 132, 51, 23, 131, 253, 58, 134, 172, 192, 32, 186, 40, 171, 219, 83, 193, 244, 74, 67, 122, 225, 165, 154, 32, 90, 127, 229, 83, 37, 193, 116 } },
                    { 3, 1, "Richard@example.com", "Doe", "Richard", "F6gz1VzSO0OhkE1CsmiSDflgEU+82504dYwRrdAaS+8=", "Manager", new byte[] { 51, 132, 51, 23, 131, 253, 58, 134, 172, 192, 32, 186, 40, 171, 219, 83, 193, 244, 74, 67, 122, 225, 165, 154, 32, 90, 127, 229, 83, 37, 193, 116 } }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_CompanyId",
                table: "users",
                column: "CompanyId");
        }
    }
}
