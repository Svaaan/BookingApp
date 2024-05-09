using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashAndSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HashedPassword", "Salt" },
                values: new object[] { "Oe6MRAWwHOmE4vBD0tsPVmTwX3QpuaAmDZd2TWs4xaQ=", new byte[] { 90, 122, 125, 74, 238, 48, 201, 231, 53, 171, 34, 123, 92, 224, 200, 87, 63, 168, 106, 104, 159, 195, 99, 34, 242, 220, 169, 196, 148, 2, 82, 245 } });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HashedPassword", "Salt" },
                values: new object[] { "Oe6MRAWwHOmE4vBD0tsPVmTwX3QpuaAmDZd2TWs4xaQ=", new byte[] { 90, 122, 125, 74, 238, 48, 201, 231, 53, 171, 34, 123, 92, 224, 200, 87, 63, 168, 106, 104, 159, 195, 99, 34, 242, 220, 169, 196, 148, 2, 82, 245 } });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HashedPassword", "Salt" },
                values: new object[] { "Oe6MRAWwHOmE4vBD0tsPVmTwX3QpuaAmDZd2TWs4xaQ=", new byte[] { 90, 122, 125, 74, 238, 48, 201, 231, 53, 171, 34, 123, 92, 224, 200, 87, 63, 168, 106, 104, 159, 195, 99, 34, 242, 220, 169, 196, 148, 2, 82, 245 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "users");
        }
    }
}
