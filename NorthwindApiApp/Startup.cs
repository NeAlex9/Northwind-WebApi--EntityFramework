using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
/*using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.DataAccess;
using Northwind.Services.Employees;
using Northwind.Services.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.Products;
using Northwind.Services.EntityFrameworkCore.ProductsService;
using Northwind.Services.EntityFrameworkCore.EmployeeService;*/

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
                /*.AddDbContextservices
                <NorthwindContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("SqlConnection")))
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IEmployeePictureService, EmployeePictureService>()
                .AddTransient<IProductCategoryService, ProductCategoryService>()
                .AddTransient<IProductCategoryPictureService, ProductCategoryPictureService>()
                .AddTransient<IEmployeeService, EmployeeService>()
                .AddTransient<IMapper, Mapper>(_ => new Mapper(new MapperConfiguration(config => config.AddProfile(new MapperProfile()))));
*/        }

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