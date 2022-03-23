using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Services.EntityFramework.Blogging.Entities
{
    /// <summary>
    /// Article blog.
    /// </summary>
    [Table("blog_article")]
    public class BlogArticleDTO
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [Key]
        [Column("blog_article_id", Order = 1, TypeName = "int")]
        public int BlogArticleId { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        [Column("title", Order = 2, TypeName = "nvarchar")]
        [MaxLength(50)]
        public string? Title { get; set; }

        /// <summary>
        /// Text.
        /// </summary>
        [Column("text", Order = 3, TypeName = "ntext")]
        public string Text { get; set; }

        /// <summary>
        /// Publication date.
        /// </summary>
        [Column("publication_date", Order = 4, TypeName = "datetime")]
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Author id.
        /// </summary>
        [Column("author_id", Order = 5, TypeName = "int")]
        public int AuthorId { get; set; }
    }
}
