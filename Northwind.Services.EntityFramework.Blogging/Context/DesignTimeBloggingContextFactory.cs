using Northwind.Services.EntityFramework.Blogging.Context;

namespace Northwind.Services.EntityFramework.Blogging.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Factory to perform migration.
    /// </summary>
    public class DesignTimeBloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    {
        /// <summary>
        /// Creates db context.
        /// </summary>
        /// <param name="args">arguments.</param>
        /// <returns>Blogging context.</returns>
        public BloggingContext CreateDbContext(string[] args)
        {
            const string connectionStringName = "NORTHWIND_BLOGGING";
            const string connectioStringPrefix = "SQLCONNSTR_";

            var configuration = new ConfigurationBuilder().
                AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString(connectionStringName);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"{connectioStringPrefix}{connectionStringName} environment variable is not set.");
            }

            Console.WriteLine($"Using {connectioStringPrefix}{connectionStringName} environment variable as a connection string.");
            var builderOptions = new DbContextOptionsBuilder<BloggingContext>().UseSqlServer(connectionString).Options;
            return new BloggingContext(builderOptions);
        }
    }
}
