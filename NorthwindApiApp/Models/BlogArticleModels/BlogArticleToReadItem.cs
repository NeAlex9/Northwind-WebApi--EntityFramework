namespace NorthwindApiApp.Models.BlogArticleModels
{
    public class BlogArticleToReadItem
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

        /// <summary>
        /// Gets or sets text.
        /// </summary>
        public string Text { get; set; }
    }
}
