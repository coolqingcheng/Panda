using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panda.Entity.Migrations
{
    public partial class update202202162037 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarkDown",
                table: "Posts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkDown",
                table: "Posts");
        }
    }
}
