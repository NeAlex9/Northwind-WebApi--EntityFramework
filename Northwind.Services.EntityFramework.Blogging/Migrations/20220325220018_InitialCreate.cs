using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Services.EntityFramework.Blogging.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog_article",
                columns: table => new
                {
                    blog_article_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    text = table.Column<string>(type: "ntext", nullable: false),
                    publication_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    author_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_article", x => x.blog_article_id);
                });

            migrationBuilder.CreateTable(
                name: "blog_article_product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    blog_article_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_article_product", x => new { x.blog_article_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_blog_article_product_blog_article_blog_article_id",
                        column: x => x.blog_article_id,
                        principalTable: "blog_article",
                        principalColumn: "blog_article_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "blog_comment",
                columns: table => new
                {
                    blog_comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    blog_article_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<string>(type: "nchar(5)", maxLength: 5, nullable: false),
                    comment = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_comment", x => new { x.blog_article_id, x.customer_id, x.blog_comment_id });
                    table.ForeignKey(
                        name: "FK_blog_comment_blog_article_blog_article_id",
                        column: x => x.blog_article_id,
                        principalTable: "blog_article",
                        principalColumn: "blog_article_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_article_product");

            migrationBuilder.DropTable(
                name: "blog_comment");

            migrationBuilder.DropTable(
                name: "blog_article");
        }
    }
}
