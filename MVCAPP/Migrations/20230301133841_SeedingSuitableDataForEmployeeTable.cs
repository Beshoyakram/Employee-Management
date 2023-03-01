using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCAPP.Migrations
{
    /// <inheritdoc />
    public partial class SeedingSuitableDataForEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbb7320d-6f08-457c-8d7e-316a171f748d", "AQAAAAEAACcQAAAAEJhp5wo31lxgObm0Ih+lzSLYMwfjlBx5LW5UJAENZseeHAtg9U3Typx3dzFIvQgg7w==", "c7f482f8-9a39-4dbb-a922-9a205bd70e66" });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Ahmed_said@gmail.com", "Ahmed said", 1255412344 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "karam@gmail.com", "Karam ahmed", 1054782510 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Monasayed@gmail.com", "Mona sayed", 1521422167 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Ramy140@gmail.com", "Ramy ayman", 1024785475 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Foaud22@gmail.com", "Foaud", 1223475496 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "sayed_ali@gmail.com", "sayed ali", 1527848784 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Samir15@gmail.com", "Samir", 1112323579 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Reem_ali@gmail.com", "Reem ali", 1001245785 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2be6680-e73f-4095-8b13-f82483c2d379", "AQAAAAEAACcQAAAAEDfP05cDHzXDcxZgUJa9Iio+W2GhHl8WRa5h2zf85G8bv6XvXJORC9APuOgg7xSn/A==", "93f8d2a3-0392-4395-bee1-5a50cea9427c" });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "remon@gmail.com", "remon", 1212222228 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Beshoy@gmail.com", "Beshoy", 1212222228 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Mona@gmail.com", "Mona", 1212222228 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Rmay@gmail.com", "Rmay", 1212222228 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Rmay2@gmail.com", "Rmay2", 1212222228 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Rmay3@gmail.com", "Rmay3", 1212222228 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Rmay4@gmail.com", "Rmay4", 1212222228 });

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "Reem@gmail.com", "Reem", 1212222228 });
        }
    }
}
