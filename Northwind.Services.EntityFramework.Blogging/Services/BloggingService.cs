using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.Blogging;
using Northwind.Services.EntityFramework.Blogging.Context;
using Northwind.Services.EntityFramework.Blogging.Entities;

namespace Northwind.Services.EntityFramework.Blogging.Services
{
    public class BloggingService : IBloggingService
    {
        private readonly BloggingContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialize a new instance of class <see cref="BloggingService"/> 
        /// </summary>
        /// <param name="context"></param>
        public BloggingService(BloggingContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <inheritdoc />
        public async IAsyncEnumerable<BlogArticle> GetAllBlogArticlesAsync()
        {
            await foreach (var blogArticleDto in this.context
                               .BlogArticles
                               .AsNoTracking()
                               .AsAsyncEnumerable())
            {
                yield return this.mapper.Map<BlogArticle>(blogArticleDto);
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<BlogArticle> GetBlogArticleAsync(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new ArgumentException("Must be greater than zero or equals zero.", nameof(offset));
            }

            if (limit < 1)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(limit));
            }

            await foreach (var blogArticleDto in this.context
                         .BlogArticles
                         .AsNoTracking()
                         .Skip(offset)
                         .Take(limit)
                         .AsAsyncEnumerable())
            {
                yield return this.mapper.Map<BlogArticle>(blogArticleDto);
            }
        }

        /// <inheritdoc />
        public async Task<(bool isSuccess, BlogArticle? blogArticle)> TryGetBlogArticleIdAsync(int blogArticleId)
        {
            var article = await this.context
                .BlogArticles
                .AsNoTracking()
                .FirstOrDefaultAsync(article => article.BlogArticleId == blogArticleId);
            return article is null ? (false, null) : (true, this.mapper.Map<BlogArticle>(article));
        }

        /// <inheritdoc />
        public async Task<int> CreateBlogArticleAsync(BlogArticle blogArticle)
        {
            ArgumentNullException.ThrowIfNull(blogArticle, nameof(blogArticle));
            var articleDto = this.mapper.Map<BlogArticleDTO>(blogArticle);
            await this.context.BlogArticles.AddAsync(articleDto);
            await this.context.SaveChangesAsync();
            return articleDto.BlogArticleId;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteBlogArticleAsync(int blogArticleId)
        {
            var dto = await this.context
                .BlogArticles
                .FirstOrDefaultAsync(dto => dto.BlogArticleId == blogArticleId);
            if (dto == null)
            {
                return false;
            }

            this.context.Remove(dto);
            return await this.context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateBlogArticleAsync(int blogArticleId, BlogArticle blogArticle)
        {
            ArgumentNullException.ThrowIfNull(blogArticle, nameof(blogArticle));
            var dto = await this.context
                .BlogArticles
                .FirstOrDefaultAsync(dto => dto.BlogArticleId == blogArticleId);
            if (dto == null)
            {
                return false;
            }

            UpdateBlogArticleDto(dto, blogArticle);
            return await this.context.SaveChangesAsync() > 0;
        }

        private static void UpdateBlogArticleDto(BlogArticleDTO dto, BlogArticle blogArticle)
        {
            dto.Title = blogArticle.Title;
            dto.Text = blogArticle.Text;
            dto.PublicationDate = blogArticle.PublicationDate;
            dto.AuthorId = blogArticle.AuthorId;
        }
    }
}
