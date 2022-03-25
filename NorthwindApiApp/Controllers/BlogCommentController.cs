using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Blogging;

namespace NorthwindApiApp.Controllers
{
    [ApiController]
    [Route("api/articles/{articleId}/comments")]
    public class BlogCommentController : ControllerBase
    {
        private IBlogCommentsService blogCommentsService;
        private IBloggingService bloggingService;

        /// <summary>
        /// Initialize a new instance of class <see cref="BlogCommentController"/>
        /// </summary>
        /// <param name="blogCommentsService"></param>
        public BlogCommentController(IBlogCommentsService blogCommentsService, IBloggingService bloggingService)
        {
            this.blogCommentsService = blogCommentsService;
            this.bloggingService = bloggingService;
        }

        [HttpGet]
        public async IAsyncEnumerable<BlogComment> GetAllCommentsRelatedToArticle([FromRoute] int articleId)
        {
            await foreach (var comment in this.blogCommentsService.GetAllAsync(articleId))
            {
                yield return comment;
            }
        }

        [HttpPut("{commentId}")]
        public async Task<ActionResult<bool>> UpdateBlogComment(BlogComment comment, [FromQuery] string customerId, [FromRoute] int articleId, [FromRoute] int commentId)
        {
            if (articleId != comment.BlogArticleId && customerId != comment.CustomerId)
            {
                return this.BadRequest();
            }

            var result = await this.blogCommentsService.UpdateAsync(comment, customerId, articleId, commentId);
            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpPost]
        public async Task<ActionResult<BlogComment>> CreateBlogComment(BlogComment comment, [FromRoute] int articleId, [FromQuery] string customerId)
        {
            var (isBlogExist, _) = await this.bloggingService.TryGetBlogArticleIdAsync(articleId);
            if (!isBlogExist)
            {
                return NotFound();
            }

            if (articleId != comment.BlogArticleId)
            {
                return BadRequest();
            }

            if (customerId != comment.CustomerId)
            {
                return BadRequest();
            }

            var (_, _, commentId) = await this.blogCommentsService.CreateAsync(comment);
            if (commentId == 0)
            {
                return NoContent();
            }

            comment.Id = commentId;
            return Ok(comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult<bool>> DeleteBlogComment([FromQuery] string customerId, [FromRoute] int articleId, [FromRoute] int commentId)
        {
            var result = await this.blogCommentsService.DeleteAsync(customerId, articleId, commentId);
            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
