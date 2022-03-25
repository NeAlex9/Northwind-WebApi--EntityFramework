using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.Blogging;
using Northwind.Services.EntityFramework.Blogging.Context;
using Northwind.Services.EntityFramework.Blogging.Entities;

namespace Northwind.Services.EntityFramework.Blogging.Services
{
    public class BlogArticleProductService : IBlogArticleProductService
    {
        private BloggingContext context;
        private IMapper mapper;

        /// <summary>
        /// Initialize a new instance if class <see cref="IBlogArticleProductService"/>
        /// </summary>
        /// <param name="context">context.</param>
        /// <param name="mapper">mapper.</param>
        public BlogArticleProductService(BloggingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<int> GetAllRelatedProductsIdAsync(int articleId)
        {
            await foreach (var productId in this.context
                                                .BlogArticleProduct
                                                .AsNoTracking()
                                                .Where(m => m.BlogArticleId == articleId)
                                                .Select(m => m.ProductId)
                                                .AsAsyncEnumerable())
            {
                yield return productId;
            }
        }

        /// <inheritdoc />
        public async Task<bool> AddLinkToProductAsync(int articleId, int productId)
        {
            var dto = new BlogArticleProductDTO()
            {
                BlogArticleId = articleId,
                ProductId = productId
            };

            this.context.Add(dto);
            return await this.context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveLinkToProductAsync(int articleId, int productId)
        {
            var dto = await this.context
                          .BlogArticleProduct
                          .FirstOrDefaultAsync(m => m.BlogArticleId == articleId && m.ProductId == productId);
            if (dto is null)
            {
                return false;
            }

            this.context.Remove(dto);
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}
