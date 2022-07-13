using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panda.Site.Migrations
{
    public partial class update20220713 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostCommentReads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CommentsId = table.Column<int>(type: "int", nullable: false),
                    AccountsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AddTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentReads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCommentReads_Accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCommentReads_PostComments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "PostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentReads_AccountsId",
                table: "PostCommentReads",
                column: "AccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentReads_CommentsId",
                table: "PostCommentReads",
                column: "CommentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostCommentReads");
        }
    }
}
