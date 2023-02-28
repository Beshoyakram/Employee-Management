using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCAPP.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "employees",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "No_image.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "employees",
                type: "int",
                maxLength: 11,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "employees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Address", "Phone", "Salary" },
                values: new object[] { "cairo", 1212222228, 3150.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "employees");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "No_image.png");
        }
    }
}
