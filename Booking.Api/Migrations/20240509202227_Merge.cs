using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Api.Migrations
{
    /// <inheritdoc />
    public partial class Merge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InterestRate",
                table: "shows",
                newName: "VAT");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HashedPassword", "Salt" },
                values: new object[] { "BizPDsh69n2K6oTCB0//QzdkPxJsgvX0A29+SL9UVqY=", new byte[] { 222, 68, 180, 109, 146, 55, 206, 161, 246, 169, 1, 76, 123, 173, 32, 40, 73, 108, 3, 182, 168, 207, 163, 52, 162, 133, 220, 197, 134, 209, 107, 95 } });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HashedPassword", "Salt" },
                values: new object[] { "BizPDsh69n2K6oTCB0//QzdkPxJsgvX0A29+SL9UVqY=", new byte[] { 222, 68, 180, 109, 146, 55, 206, 161, 246, 169, 1, 76, 123, 173, 32, 40, 73, 108, 3, 182, 168, 207, 163, 52, 162, 133, 220, 197, 134, 209, 107, 95 } });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HashedPassword", "Salt" },
                values: new object[] { "BizPDsh69n2K6oTCB0//QzdkPxJsgvX0A29+SL9UVqY=", new byte[] { 222, 68, 180, 109, 146, 55, 206, 161, 246, 169, 1, 76, 123, 173, 32, 40, 73, 108, 3, 182, 168, 207, 163, 52, 162, 133, 220, 197, 134, 209, 107, 95 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VAT",
                table: "shows",
                newName: "InterestRate");

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
    }
}
