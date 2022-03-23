using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Blogging
{
    public interface IBloggingService
    {
        /// <summary>
        /// Gets all articles.
        /// </summary>
        /// <returns>list of articles.</returns>
        IAsyncEnumerable<BlogArticle> GetAllBlogArticles();

        /// <summary>
        /// Shows a list of blog articles using specified offset and limit for pagination.
        /// </summary>
        /// <param name="offset">An offset of the first element to return.</param>
        /// <param name="limit">A limit of elements to return.</param>
        /// <returns>A <see cref="IAsyncEnumerable{T}"/> of <see cref="BlogArticle"/>.</returns>
        IAsyncEnumerable<BlogArticle> GetBlogArticleAsync(int offset, int limit);

        /// <summary>
        /// Try to show a employee with specified identifier.
        /// </summary>
        /// <param name="blogArticleId">A product identifier.</param>
        /// <returns>Returns true if a product is returned; otherwise false.</returns>
        Task<(bool isSuccess, BlogArticle? blogArticle)> TryGetBlogArticleIdAsync(int blogArticleId);

        /// <summary>
        /// Creates a new blog article.
        /// </summary>
        /// <param name="blogArticle">A <see cref="BlogArticle"/> to create.</param>
        /// <returns>An identifier of a created product.</returns>
        Task<int> CreateBlogArticleAsync(BlogArticle blogArticle);

        /// <summary>
        /// Destroys an existed blog article.
        /// </summary>
        /// <param name="blogArticleId">A product identifier.</param>
        /// <returns>True if a product is destroyed; otherwise false.</returns>
        Task<bool> DeleteBlogArticleAsync(int blogArticleId);

        /// <summary>
        /// Updates a blog article.
        /// </summary>
        /// <param name="blogArticleId">A blog article identifier.</param>
        /// <param name="blogArticle">A <see cref="BlogArticle"/>.</param>
        /// <returns>True if a blog article is updated; otherwise false.</returns>
        Task<bool> UpdateBlogArticleAsync(int blogArticleId, BlogArticle blogArticle);
    }
}
