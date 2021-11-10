using Microsoft.EntityFrameworkCore.Migrations;

namespace Panda.Entity.Migrations
{
    public partial class update2111102351 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomLink",
                table: "Posts",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CustomLink",
                table: "Posts",
                column: "CustomLink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_CustomLink",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CustomLink",
                table: "Posts");
        }
    }
}
