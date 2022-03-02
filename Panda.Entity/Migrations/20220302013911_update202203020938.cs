using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panda.Entity.Migrations
{
    public partial class update202203020938 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_CustomLink",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "categoryName",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.AlterColumn<string>(
                name: "CustomLink",
                table: "Posts",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CustomLink",
                table: "Posts",
                column: "CustomLink",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_CustomLink",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "categoryName");

            migrationBuilder.AlterColumn<string>(
                name: "CustomLink",
                table: "Posts",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CustomLink",
                table: "Posts",
                column: "CustomLink");
        }
    }
}
