using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Services.EntityFramework.Blogging.Migrations
{
    public partial class AddBlogComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog_comment",
                columns: table => new
                {
                    blog_comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "ntext", nullable: false),
                    customer_id = table.Column<string>(type: "nchar(5)", maxLength: 5, nullable: false),
                    blog_article_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_comment", x => x.blog_comment_id);
                    table.ForeignKey(
                        name: "FK_blog_comment_blog_article_blog_article_id",
                        column: x => x.blog_article_id,
                        principalTable: "blog_article",
                        principalColumn: "blog_article_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blog_comment_blog_article_id",
                table: "blog_comment",
                column: "blog_article_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_comment");
        }
    }
}
