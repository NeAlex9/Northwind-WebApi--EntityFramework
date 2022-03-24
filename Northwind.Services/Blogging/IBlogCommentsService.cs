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
        Task<int> Create(BlogComment blogComment, int articleId);

        /// <summary>
        /// Update blog comment.
        /// </summary>
        /// <param name="blogComment">comment data to update.</param>
        /// <param name="blogCommentId">id.</param>
        /// <returns>Is successful.</returns>
        Task<bool> Update(BlogComment blogComment, int blogCommentId);

        /// <summary>
        /// Delete blog comment.
        /// </summary>
        /// <param name="blogCommentId">id.</param>
        /// <returns>Is successful.</returns>
        Task<bool> Delete(int blogCommentId);

        /// <summary>
        /// Gets all comments related to article.
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<BlogComment> GetAll(int articleId);
    }
}
