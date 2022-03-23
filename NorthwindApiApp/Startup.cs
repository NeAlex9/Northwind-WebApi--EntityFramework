using System.Data.SqlClient;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Services.Blogging;
using Northwind.Services.Employees;
using Northwind.Services.EntityFramework.Blogging;
using Northwind.Services.EntityFramework.Blogging.Context;
using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.EntityFrameworkCore.EmployeeService;
using Northwind.Services.EntityFrameworkCore.ProductsService;
using Northwind.Services.Products;

namespace NorthwindApiApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);
            services
                .AddSwaggerGen();
            services
                .AddDbContext<BloggingContext>(options => options.UseSqlServer(@"data source=(localdb)\MSSQLLocalDB; Integrated Security=True; Initial Catalog=Blogging;"))
                .AddDbContext<NorthwindContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("SqlConnection")))
                .AddTransient<IBloggingService, BloggingService>(provider => new BloggingService(provider.GetService<BloggingContext>()!, provider.GetServices<IMapper>().ElementAt(0)))
                .AddTransient<IProductService, ProductService>(provider => new ProductService(provider.GetService<NorthwindContext>()!, provider.GetServices<IMapper>().ElementAt(1)))
                .AddTransient<IEmployeePictureService, EmployeePictureService>()
                .AddTransient<IProductCategoryService, ProductCategoryService>(provider => new ProductCategoryService(provider.GetService<NorthwindContext>()!, provider.GetServices<IMapper>().ElementAt(1)))
                .AddTransient<IProductCategoryPictureService, ProductCategoryPictureService>()
                .AddTransient<IEmployeeService, EmployeeService>(provider => new EmployeeService(provider.GetService<NorthwindContext>()!, provider.GetServices<IMapper>().ElementAt(1)))
                .AddTransient<IMapper, Mapper>(_ => new Mapper(new MapperConfiguration(config => config.AddProfile(new Northwind.Services.EntityFramework.Blogging.MapperProfile()))))
                .AddTransient<IMapper, Mapper>(_ => new Mapper(new MapperConfiguration(config => config.AddProfile(new Northwind.Services.EntityFrameworkCore.MapperProfile()))))
                .AddTransient<IMapper, Mapper>(_ => new Mapper(new MapperConfiguration(config => config.AddProfile(new NorthwindApiApp.MapperInfo.MapperProfile()))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI((app) =>
                {
                    app.SwaggerEndpoint("/swagger/v1/swagger.json", "Custom");
                });
                app.UseDeveloperExceptionPage();
            }

            var configuration = new ConfigurationBuilder().
                AddEnvironmentVariables()
                .Build();

            const string connectionStringName = "NORTHWIND_BLOGGING";

            var connectionString = configuration.GetConnectionString(connectionStringName);


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}