using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Services.EntityFramework.Blogging.Entities
{
    /// <summary>
    /// Blog Comments dto.
    /// </summary>
    [Table("blog_comment")]
    public class BlogCommentDTO
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [Column("blog_comment_id", Order = 1, TypeName = "int")]
        public int BlogCommentId { get; set; }

        /// <summary>
        /// Gets or  sets comment.
        /// </summary>
        [Column("comment", Order = 2, TypeName = "ntext")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets customer id.
        /// </summary>
        [Column("customer_id", Order = 3, TypeName = "nchar")]
        [StringLength(5)]
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets blog article id.
        /// </summary>
        [Column("blog_article_id", Order = 4, TypeName = "int")]
        [ForeignKey("BlogArticles")]
        public int BlogArticleId { get; set; }

        /// <summary>
        /// Navigation property blog articles.
        /// </summary>
        public virtual BlogArticleDTO BlogArticle { get; set; }
    }
}
