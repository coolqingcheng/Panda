using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QingCheng.Site.Migrations
{
    /// <inheritdoc />
    public partial class update2302082158 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    rolename = table.Column<string>(name: "role_name", type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_roles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)"),
                    username = table.Column<string>(name: "user_name", type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pwd = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lockedtime = table.Column<DateTime>(name: "locked_time", type: "datetime(6)", nullable: false),
                    loginfailcount = table.Column<int>(name: "login_fail_count", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "friend_links",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: false),
                    ispublish = table.Column<bool>(name: "is_publish", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_friend_links", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_cates",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    catename = table.Column<string>(name: "cate_name", type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_cates", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tagname = table.Column<string>(name: "tag_name", type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_tags", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    thumb = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    snippet = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    readcount = table.Column<int>(name: "read_count", type: "int", nullable: false),
                    ispublish = table.Column<bool>(name: "is_publish", type: "tinyint(1)", nullable: false),
                    istop = table.Column<bool>(name: "is_top", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)"),
                    isdelete = table.Column<bool>(name: "is_delete", type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_config",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    key = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    groupname = table.Column<string>(name: "group_name", type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_config", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_login_record",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    accountid = table.Column<Guid>(name: "account_id", type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ua = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ip = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    islogin = table.Column<bool>(name: "is_login", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_login_record", x => x.id);
                    table.ForeignKey(
                        name: "fk_account_login_record_accounts_account_id",
                        column: x => x.accountid,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_role_relation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    accountid = table.Column<Guid>(name: "account_id", type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    accountroleid = table.Column<Guid>(name: "account_role_id", type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_role_relation", x => x.id);
                    table.ForeignKey(
                        name: "fk_account_role_relation_account_roles_account_role_id",
                        column: x => x.accountroleid,
                        principalTable: "account_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_account_role_relation_accounts_account_id",
                        column: x => x.accountid,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_resources",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    accountid = table.Column<Guid>(name: "account_id", type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    suffix = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clientip = table.Column<string>(name: "client_ip", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    storagetype = table.Column<int>(name: "storage_type", type: "int", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_resources", x => x.id);
                    table.ForeignKey(
                        name: "fk_sys_resources_accounts_account_id",
                        column: x => x.accountid,
                        principalTable: "accounts",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_cate_relations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    postcateid = table.Column<int>(name: "post_cate_id", type: "int", nullable: false),
                    postsid = table.Column<int>(name: "posts_id", type: "int", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_cate_relations", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_cate_relations_post_cates_post_cate_id",
                        column: x => x.postcateid,
                        principalTable: "post_cates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_cate_relations_posts_posts_id",
                        column: x => x.postsid,
                        principalTable: "posts",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nickname = table.Column<string>(name: "nick_name", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pid = table.Column<int>(type: "int", nullable: false),
                    postsid = table.Column<int>(name: "posts_id", type: "int", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_comments_posts_posts_id",
                        column: x => x.postsid,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_tag_relations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    postsid = table.Column<int>(name: "posts_id", type: "int", nullable: false),
                    posttagsid = table.Column<int>(name: "post_tags_id", type: "int", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_tag_relations", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_tag_relations_post_tags_post_tags_id",
                        column: x => x.posttagsid,
                        principalTable: "post_tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_tag_relations_posts_posts_id",
                        column: x => x.postsid,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_visit_record",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    postid = table.Column<int>(name: "post_id", type: "int", nullable: false),
                    ip = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ua = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uid = table.Column<string>(name: "u_id", type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_visit_record", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_visit_record_posts_post_id",
                        column: x => x.postid,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_account_login_record_account_id",
                table: "account_login_record",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_account_role_relation_account_id",
                table: "account_role_relation",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_account_role_relation_account_role_id",
                table: "account_role_relation",
                column: "account_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_cate_relations_post_cate_id",
                table: "post_cate_relations",
                column: "post_cate_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_cate_relations_posts_id",
                table: "post_cate_relations",
                column: "posts_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_comments_posts_id",
                table: "post_comments",
                column: "posts_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_tag_relations_post_tags_id",
                table: "post_tag_relations",
                column: "post_tags_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_tag_relations_posts_id",
                table: "post_tag_relations",
                column: "posts_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_visit_record_post_id",
                table: "post_visit_record",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_sys_config_key",
                table: "sys_config",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sys_resources_account_id",
                table: "sys_resources",
                column: "account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_login_record");

            migrationBuilder.DropTable(
                name: "account_role_relation");

            migrationBuilder.DropTable(
                name: "friend_links");

            migrationBuilder.DropTable(
                name: "post_cate_relations");

            migrationBuilder.DropTable(
                name: "post_comments");

            migrationBuilder.DropTable(
                name: "post_tag_relations");

            migrationBuilder.DropTable(
                name: "post_visit_record");

            migrationBuilder.DropTable(
                name: "sys_config");

            migrationBuilder.DropTable(
                name: "sys_resources");

            migrationBuilder.DropTable(
                name: "account_roles");

            migrationBuilder.DropTable(
                name: "post_cates");

            migrationBuilder.DropTable(
                name: "post_tags");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
