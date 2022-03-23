using System.Text.Json.Serialization;

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
        [JsonPropertyOrder(0)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets posted date time in string format;
        /// </summary>
        [JsonPropertyOrder(2)]
        public string Posted { get; set; }

        /// <summary>
        /// Gets or sets author id.
        /// </summary>
        [JsonPropertyOrder(3)]
        public int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets author name.
        /// </summary>
        [JsonPropertyOrder(4)]
        public string AuthorName { get; set; }
    }
}
