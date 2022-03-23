using System.Text.Json.Serialization;

namespace NorthwindApiApp.Models.BlogArticleModels
{
    public class BlogArticleToReadItem
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
        [JsonPropertyOrder(4)]
        public int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets author name.
        /// </summary>
        [JsonPropertyOrder(5)]
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets text.
        /// </summary>
        [JsonPropertyOrder(3)]
        public string Text { get; set; }
    }
}
