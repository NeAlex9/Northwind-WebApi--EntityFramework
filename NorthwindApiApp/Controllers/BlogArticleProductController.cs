using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Blogging;
using Northwind.Services.Products;

namespace NorthwindApiApp.Controllers
{
    [ApiController]
    [Route("api/articles/{articleId}/products")]
    public class BlogArticleProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IBlogArticleProductService blogArticleProductService;
        private readonly IBloggingService bloggingService;

        public BlogArticleProductController(IProductService productService, IBlogArticleProductService blogArticleProductService, IBloggingService bloggingService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.blogArticleProductService = blogArticleProductService ?? throw new ArgumentNullException(nameof(blogArticleProductService));
            this.bloggingService = bloggingService ?? throw new ArgumentNullException(nameof(bloggingService));
        }

        [HttpGet]
        public async IAsyncEnumerable<Product> GetRelatedProducts([FromRoute]int articleId)
        {
            await foreach (var productId in this.blogArticleProductService.GetAllRelatedProductsIdAsync(articleId))
            {
                var (isSuccess, product) = await this.productService.TryGetProductAsync(productId);
                if (isSuccess)
                {
                    yield return product;
                }
            }
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddLinkToProduct([FromRoute]int productId, [FromRoute]int articleId)
        {
            var (isProductExist, _) = await this.productService.TryGetProductAsync(productId);
            if (!isProductExist)
            {
                return NotFound();
            }

            var (isArticleExist, _) = await this.bloggingService.TryGetBlogArticleIdAsync(articleId);
            if (!isArticleExist)
            {
                return NotFound();
            }

            var isSuccess = await this.blogArticleProductService.AddLinkToProductAsync(articleId, productId);
            if (!isSuccess)
            {
                 return NoContent();
            }

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveLinkToProduct([FromRoute] int productId, [FromRoute] int articleId)
        {
            var isSuccess = await this.blogArticleProductService.RemoveLinkToProductAsync(articleId, productId);
            if (!isSuccess)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
