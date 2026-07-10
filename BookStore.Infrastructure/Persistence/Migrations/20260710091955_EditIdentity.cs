using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "019f4b14-a55c-7327-b3f2-3875a5147ea4", "019f4b06-0a78-70e9-aee1-9e0861a695c0" },
                    { "019f4b14-a55c-7327-b3f2-3877b4e85640", "019f4b09-c8b9-74ef-9a6b-157523f014b6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019f4b14-a55c-7327-b3f2-3875a5147ea4", "019f4b06-0a78-70e9-aee1-9e0861a695c0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019f4b14-a55c-7327-b3f2-3877b4e85640", "019f4b09-c8b9-74ef-9a6b-157523f014b6" });

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "RefreshTokens");
        }
    }
}
