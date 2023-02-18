using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QingCheng.Site.Migrations
{
    /// <inheritdoc />
    public partial class update2302082333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_cate_relations_posts_posts_id",
                table: "post_cate_relations");

            migrationBuilder.DropForeignKey(
                name: "fk_post_comments_posts_posts_id",
                table: "post_comments");

            migrationBuilder.DropForeignKey(
                name: "fk_post_tag_relations_posts_posts_id",
                table: "post_tag_relations");

            migrationBuilder.RenameColumn(
                name: "posts_id",
                table: "post_tag_relations",
                newName: "post_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_tag_relations_posts_id",
                table: "post_tag_relations",
                newName: "ix_post_tag_relations_post_id");

            migrationBuilder.RenameColumn(
                name: "posts_id",
                table: "post_comments",
                newName: "post_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_comments_posts_id",
                table: "post_comments",
                newName: "ix_post_comments_post_id");

            migrationBuilder.RenameColumn(
                name: "posts_id",
                table: "post_cate_relations",
                newName: "post_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_cate_relations_posts_id",
                table: "post_cate_relations",
                newName: "ix_post_cate_relations_post_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_time",
                table: "post_comments",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_time",
                table: "post_comments",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddForeignKey(
                name: "fk_post_cate_relations_posts_post_id",
                table: "post_cate_relations",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_comments_posts_post_id",
                table: "post_comments",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_tag_relations_posts_post_id",
                table: "post_tag_relations",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_cate_relations_posts_post_id",
                table: "post_cate_relations");

            migrationBuilder.DropForeignKey(
                name: "fk_post_comments_posts_post_id",
                table: "post_comments");

            migrationBuilder.DropForeignKey(
                name: "fk_post_tag_relations_posts_post_id",
                table: "post_tag_relations");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "post_tag_relations",
                newName: "posts_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_tag_relations_post_id",
                table: "post_tag_relations",
                newName: "ix_post_tag_relations_posts_id");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "post_comments",
                newName: "posts_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_comments_post_id",
                table: "post_comments",
                newName: "ix_post_comments_posts_id");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "post_cate_relations",
                newName: "posts_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_cate_relations_post_id",
                table: "post_cate_relations",
                newName: "ix_post_cate_relations_posts_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_time",
                table: "post_comments",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_time",
                table: "post_comments",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddForeignKey(
                name: "fk_post_cate_relations_posts_posts_id",
                table: "post_cate_relations",
                column: "posts_id",
                principalTable: "posts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_comments_posts_posts_id",
                table: "post_comments",
                column: "posts_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_tag_relations_posts_posts_id",
                table: "post_tag_relations",
                column: "posts_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
