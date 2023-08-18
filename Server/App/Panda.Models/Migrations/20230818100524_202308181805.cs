using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panda.Models.Migrations
{
    /// <inheritdoc />
    public partial class _202308181805 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreateTime", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("2bb0c71e-24d5-4e62-981d-e8741cbbb97c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "chenxinandroid@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2bb0c71e-24d5-4e62-981d-e8741cbbb97c"));
        }
    }
}
