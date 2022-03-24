namespace Northwind.Services.Blogging
{
    /// <summary>
    /// Blog comment.
    /// </summary>
    public class BlogComment
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or  sets comment.
        /// </summary>
        public string Comment { get; set; }
        
        /// <summary>
        /// Gets or sets customer id.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets blog article id.
        /// </summary>
        public int BlogArticleId { get; set; }
    }
}
