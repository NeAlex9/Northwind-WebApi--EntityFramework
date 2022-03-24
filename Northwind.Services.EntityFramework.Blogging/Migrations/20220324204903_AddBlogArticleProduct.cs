using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Services.EntityFramework.Blogging.Migrations
{
    public partial class AddBlogArticleProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog_article_product",
                columns: table => new
                {
                    blog_article_product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    blog_article_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_article_product", x => x.blog_article_product_id);
                    table.ForeignKey(
                        name: "FK_blog_article_product_blog_article_blog_article_id",
                        column: x => x.blog_article_id,
                        principalTable: "blog_article",
                        principalColumn: "blog_article_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blog_article_product_blog_article_id",
                table: "blog_article_product",
                column: "blog_article_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_article_product");
        }
    }
}
