using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Blogging;
using Northwind.Services.Employees;
using NorthwindApiApp.MapperInfo;
using NorthwindApiApp.Models.BlogArticleModel;
using NorthwindApiApp.Models.BlogArticleModels;

namespace NorthwindApiApp.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class BlogArticlesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IBloggingService bloggingService;
        private readonly IMapper mapper;

        public BlogArticlesController(IEmployeeService employeeService, IBloggingService bloggingService)
        {
            this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            this.bloggingService = bloggingService ?? throw new ArgumentNullException(nameof(bloggingService));
            this.mapper = new Mapper(new MapperConfiguration(config => config.AddProfile(new NorthwindApiApp.MapperInfo.MapperProfile())));
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<BlogArticleToReadItem>> GetBlogArticleById([FromRoute]int id)
        {
            (bool isSuccess1, BlogArticle? blogArticle) = await this.bloggingService.TryGetBlogArticleIdAsync(id);
            if (!isSuccess1)
            {
                return NotFound();
            }

            (bool isSuccess2, Employee employee) = await this.employeeService.TryGetEmployeeIdAsync(blogArticle!.AuthorId);
            if (!isSuccess2)
            {
                return BadRequest();
            }

            return this.mapper.Map<Employee, BlogArticle, BlogArticleToReadItem>(employee, blogArticle);
        }

        [HttpGet("{offset}/{limit}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async IAsyncEnumerable<BlogArticleToReadAllItems> GetBlogArticles(int offset, int limit)
        {
            await foreach (var blogArticle in this.bloggingService
                               .GetBlogArticleAsync(offset, limit))
            {
                (bool isSuccess2, Employee employee) = await this.employeeService.TryGetEmployeeIdAsync(blogArticle!.AuthorId);
                yield return this.mapper.Map<Employee, BlogArticle, BlogArticleToReadAllItems>(employee, blogArticle);
            }
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async IAsyncEnumerable<BlogArticleToReadAllItems> GetAllBlogArticles()
        {
            await foreach (var blogArticle in this.bloggingService
                               .GetAllBlogArticlesAsync())
            {
                (bool isSuccess2, Employee employee) = await this.employeeService.TryGetEmployeeIdAsync(blogArticle!.AuthorId);
                yield return this.mapper.Map<Employee, BlogArticle, BlogArticleToReadAllItems>(employee, blogArticle);
            }
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Employee>> CreateBlogArticle(BlogArticleToCreate blogArticleToCreate)
        {
            var (isSuccessful, employee) = await this.employeeService.TryGetEmployeeIdAsync(blogArticleToCreate.AuthorId);
            if (!isSuccessful)
            {
                return BadRequest();
            }

            var blogArticle = this.mapper.Map<BlogArticle>(blogArticleToCreate);
            blogArticle.PublicationDate = DateTime.Now;
            var blogArticleId = await this.bloggingService.CreateBlogArticleAsync(blogArticle);
            blogArticle.Id = blogArticleId;
            return this.CreatedAtAction(nameof(this.GetBlogArticleById), new
            {
                id = blogArticle.Id
            }, blogArticle);
        }

        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteBlogArticle(int id)
        {
            var result = await this.bloggingService
                .DeleteBlogArticleAsync(id);
            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> UpdateBlogArticle(int id, BlogArticleToUpdate blogArticleToUpdate)
        {
            var blogArticle = this.mapper.Map<BlogArticle>(blogArticleToUpdate);
            blogArticle.PublicationDate = DateTime.Now;
            var result = await this.bloggingService.UpdateBlogArticleAsync(id, blogArticle);
            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
