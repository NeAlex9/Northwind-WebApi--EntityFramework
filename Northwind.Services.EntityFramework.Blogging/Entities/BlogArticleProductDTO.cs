using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Services.EntityFramework.Blogging.Entities
{
    [Table("blog_article_product")]
    public class BlogArticleProductDTO
    {
        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        [Column("product_id", Order = 1, TypeName = "int")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets blog article id.
        /// </summary>
        [Column("blog_article_id", Order = 2, TypeName = "int")]
        [ForeignKey("BlogArticle")]
        public int BlogArticleId { get; set; }

        /// <summary>
        /// Gets or sets navigational property.
        /// </summary>
        public BlogArticleDTO BlogArticle { get; set; }
    }
}
