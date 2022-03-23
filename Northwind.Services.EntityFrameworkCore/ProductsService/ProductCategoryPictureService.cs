using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.Products;

namespace Northwind.Services.EntityFrameworkCore.ProductsService
{
    public class ProductCategoryPictureService : IProductCategoryPictureService
    {
        private readonly NorthwindContext context;
        private const int ReservedBytes = 78;

        public ProductCategoryPictureService(NorthwindContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<(bool IsSuccess, byte[] imageBytes)> TryGetPictureAsync(int categoryId)
        {
            var dto = await this.context
                .Categories
                .AsNoTracking()
                .Where(category => category.CategoryId == categoryId)
                .FirstOrDefaultAsync();
            if (dto is null || dto.Picture is null)
            {
                return (false, new byte[] { });
            }

            return (true, dto.Picture[ReservedBytes..]);
        }

        /// <inheritdoc />
        public async Task<bool> UpdatePictureAsync(int categoryId, Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));
            var dto = await this.context
                .Categories
                .Where(category => category.CategoryId == categoryId)
                .FirstOrDefaultAsync();
            if (dto is null)
            {
                return false;
            }

            await using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            bytes.CopyTo(dto.Picture, ReservedBytes);

            var updatedRows = await this.context.SaveChangesAsync();
            return updatedRows > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeletePictureAsync(int categoryId)
        {
            var dto = await this.context
                .Categories
                .Where(category => category.CategoryId == categoryId)
                .FirstOrDefaultAsync();
            if (dto is null)
            {
                return false;
            }

            dto.Picture = null;
            var updatedRows = await this.context.SaveChangesAsync();
            return updatedRows > 0;
        }
    }
}
