namespace NorthwindApiApp.Models.BlogArticleModel
{
    /// <summary>
    /// Blog article passed to controller to perform create operation.
    /// </summary>
    public class BlogArticleToCreate
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets author id.
        /// </summary>
        public int AuthorId { get; set; }
    }
}
