using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.Blogging;
using Northwind.Services.EntityFramework.Blogging.Context;
using Northwind.Services.EntityFramework.Blogging.Entities;

namespace Northwind.Services.EntityFramework.Blogging.Services
{
    /// <inheritdoc cref="IBlogCommentsService"/>
    public class BlogCommentService : IBlogCommentsService
    {
        private readonly BloggingContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialize a new instance of class <see cref="BlogCommentService"/>
        /// </summary>
        /// <param name="context">context.</param>
        /// <param name="mapper">mapper.</param>
        public BlogCommentService(BloggingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<(string customerId, int articleId, int commentId)> CreateAsync(BlogComment blogComment)
        {
            ArgumentNullException.ThrowIfNull(blogComment, nameof(blogComment));
            var blogCommentDto = this.mapper.Map<BlogCommentDTO>(blogComment);
            await this.context.BlogComments.AddAsync(blogCommentDto);
            await this.context.SaveChangesAsync();
            return (blogCommentDto.CustomerId, blogCommentDto.BlogArticleId, blogCommentDto.BlogCommentId);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(BlogComment blogComment, string customerId, int blogArticleId, int commentId)
        {
            ArgumentNullException.ThrowIfNull(blogComment, nameof(blogComment));
            var dto = await this.context
                                .BlogComments
                                .FirstOrDefaultAsync(dto => dto.CustomerId == customerId && dto.BlogArticleId == blogArticleId && dto.BlogCommentId == commentId);
            if (dto == null)
            {
                return false;
            }

            UpdateBlogCommentDto(dto, blogComment);
            return await this.context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(string customerId, int blogArticleId, int commentId)
        {
            var dto = await this.context
                                .BlogComments
                                .FirstOrDefaultAsync(dto => dto.CustomerId == customerId && dto.BlogArticleId == blogArticleId && dto.BlogCommentId == commentId);
            if (dto == null)
            {
                return false;
            }

            this.context.Remove(dto);
            return await this.context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<BlogComment> GetAllAsync(int articleId)
        {
            await foreach (var blogCommentDto in this.context
                                                     .BlogComments
                                                     .AsNoTracking()
                                                     .Where(comment => comment.BlogArticleId == articleId)
                                                     .AsAsyncEnumerable())
            {
                yield return this.mapper.Map<BlogComment>(blogCommentDto);
            }
        }

        private void UpdateBlogCommentDto(BlogCommentDTO dto, BlogComment blogComment)
        {
            dto.Comment = blogComment.Comment;
        }
    }
}
