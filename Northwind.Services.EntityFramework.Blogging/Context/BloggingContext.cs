using Northwind.Services.EntityFramework.Blogging.Entities;

namespace Northwind.Services.EntityFramework.Blogging.Context
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Blogging db context.
    /// </summary>
    public class BloggingContext : DbContext
    {
        public BloggingContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BloggingContext"/> class.
        /// </summary>
        /// <param name="options">configure instance.</param>
        public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// Gets or sets blog articles.
        /// </summary>
        public virtual DbSet<BlogArticleDTO> BlogArticles { get; set; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = @"data source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=Blogging;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
