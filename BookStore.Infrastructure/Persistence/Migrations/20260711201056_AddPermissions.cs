using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "users:read", "019f4b14-a55c-7327-b3f2-3875a5147ea4" },
                    { 2, "permissions", "users:add", "019f4b14-a55c-7327-b3f2-3875a5147ea4" },
                    { 3, "permissions", "users:update", "019f4b14-a55c-7327-b3f2-3875a5147ea4" },
                    { 4, "permissions", "roles:read", "019f4b14-a55c-7327-b3f2-3875a5147ea4" },
                    { 5, "permissions", "roles:add", "019f4b14-a55c-7327-b3f2-3875a5147ea4" },
                    { 6, "permissions", "roles:update", "019f4b14-a55c-7327-b3f2-3875a5147ea4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
