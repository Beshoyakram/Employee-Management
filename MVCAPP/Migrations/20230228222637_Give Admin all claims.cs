using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCAPP.Migrations
{
    /// <inheritdoc />
    public partial class GiveAdminallclaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Create Role", "true", "02174cf0–9412–4cfe-afbf-59f706d72cf6" },
                    { 2, "Edit Role", "true", "02174cf0–9412–4cfe-afbf-59f706d72cf6" },
                    { 3, "Delete Role", "true", "02174cf0–9412–4cfe-afbf-59f706d72cf6" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2be6680-e73f-4095-8b13-f82483c2d379", "AQAAAAEAACcQAAAAEDfP05cDHzXDcxZgUJa9Iio+W2GhHl8WRa5h2zf85G8bv6XvXJORC9APuOgg7xSn/A==", "93f8d2a3-0392-4395-bee1-5a50cea9427c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51dbcbce-25c1-47d4-8658-d10dfb2d803f", "AQAAAAEAACcQAAAAEOycaKOc9+sKFWbV7Pfc0v9acxO9ZhqPI1dABoyjKy4R9v4SPIY0i654P0CCEHj0tQ==", "3baf7316-bd31-4a0a-9d85-354578eaf766" });
        }
    }
}
