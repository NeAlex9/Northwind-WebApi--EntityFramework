using Northwind.Services.Products;

namespace Northwind.Services.Blogging
{
    /// <summary>
    /// Service to perform operation with articles and products.
    /// </summary>
    public interface IBlogArticleProductService
    {
        /// <summary>
        /// Gets all related to article products.
        /// </summary>
        /// <param name="articleId">article id.</param>
        /// <returns>collection of products.</returns>
        IAsyncEnumerable<Product> GetAllRelatedProducts(int articleId);

        /// <summary>
        /// Creates a link from article to product.
        /// </summary>
        /// <param name="articleId">article id.</param>
        /// <param name="productId">product id.</param>
        /// <returns>Is successful.</returns>
        Task<bool> AddLinkToProduct(int articleId, int productId);

        /// <summary>
        /// Removes a link from article to product.
        /// </summary>
        /// <param name="articleId">article id.</param>
        /// <param name="productId">product id.</param>
        /// <returns>Is successful.</returns>
        Task<bool> RemoveLinkToProduct(int articleId, int productId);
    }
}
