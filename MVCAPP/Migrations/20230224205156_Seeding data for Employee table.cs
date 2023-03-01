using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCAPP.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforEmployeetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhotoPath",
                value: "Male1.png");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "PhotoPath" },
                values: new object[] { "Beshoy@gmail.com", "Male2.png" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[,]
                {
                    { 3, 1, "Mona@gmail.com", "Mona", "Female1.png" },
                    { 4, 1, "Rmay@gmail.com", "Rmay", "Male2.png" },
                    { 5, 1, "Rmay2@gmail.com", "Rmay2", "Male2.png" },
                    { 6, 1, "Rmay3@gmail.com", "Rmay3", "Male2.png" },
                    { 7, 1, "Rmay4@gmail.com", "Rmay4", "Male2.png" },
                    { 8, 1, "Reem@gmail.com", "Reem", "Female1.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhotoPath",
                value: null);

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "PhotoPath" },
                values: new object[] { "beshoy@gmail.com", null });
        }
    }
}
