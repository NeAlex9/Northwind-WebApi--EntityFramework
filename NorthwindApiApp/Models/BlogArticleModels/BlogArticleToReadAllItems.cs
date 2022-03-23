namespace NorthwindApiApp.Models.BlogArticleModels
{
    /// <summary>
    /// Blog article passed to controller to perform read all items operation.
    /// </summary>
    public class BlogArticleToReadAllItems
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets posted date time in string format;
        /// </summary>
        public string Posted { get; set; }

        /// <summary>
        /// Gets or sets author id.
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets author name.
        /// </summary>
        public string AuthorName { get; set; }
    }
}
