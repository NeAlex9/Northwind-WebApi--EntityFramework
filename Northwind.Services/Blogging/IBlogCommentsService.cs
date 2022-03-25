namespace Northwind.Services.Blogging
{
    /// <summary>
    /// Service to perform operation with blog comments.
    /// </summary>
    public interface IBlogCommentsService
    {
        /// <summary>
        /// Create blog comment.
        /// </summary>
        /// <param name="blogComment"> blog comment to create.</param>
        /// <param name="articleId">article id.</param>
        /// <returns>created comment id.</returns>
        Task<(string customerId, int articleId, int commentId)> CreateAsync(BlogComment blogComment);

        /// <summary>
        /// Update blog comment.
        /// </summary>
        /// <param name="blogComment">comment data to update.</param>
        /// <param name="blogCommentId">id.</param>
        /// <returns>Is successful.</returns>
        Task<bool> UpdateAsync(BlogComment blogComment, string customerId, int blogArticleId, int commentId);

        /// <summary>
        /// Delete blog comment.
        /// </summary>
        /// <param name="blogCommentId">id.</param>
        /// <returns>Is successful.</returns>
        Task<bool> DeleteAsync(string customerId, int blogArticleId, int commentId);

        /// <summary>
        /// Gets all comments related to article.
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<BlogComment> GetAllAsync(int articleId);
    }
}
