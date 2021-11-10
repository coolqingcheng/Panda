using Microsoft.EntityFrameworkCore.Migrations;

namespace Panda.Entity.Migrations
{
    public partial class update20211109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowComment",
                table: "Posts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Id_Status",
                table: "Posts",
                columns: new[] { "Id", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UpdateTime",
                table: "Posts",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_Id_Status",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UpdateTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AllowComment",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Posts");
        }
    }
}
