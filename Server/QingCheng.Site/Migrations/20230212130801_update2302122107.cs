using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QingCheng.Site.Migrations
{
    /// <inheritdoc />
    public partial class update2302122107 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "storage_domain",
                table: "sys_resources",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "storage_domain",
                table: "sys_resources");
        }
    }
}
