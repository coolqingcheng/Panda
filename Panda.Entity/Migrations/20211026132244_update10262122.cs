using Microsoft.EntityFrameworkCore.Migrations;

namespace Panda.Entity.Migrations
{
    public partial class update10262122 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlName",
                table: "Pages",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Pages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Pages",
                newName: "UrlName");
        }
    }
}
