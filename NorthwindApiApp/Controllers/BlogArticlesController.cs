using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Blogging;
using Northwind.Services.Employees;
using Northwind.Services.Products;

namespace NorthwindApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogArticlesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IBloggingService bloggingService;

        public BlogArticlesController(IEmployeeService employeeService, IBloggingService bloggingService)
        {
            this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            this.bloggingService = bloggingService ?? throw new ArgumentNullException(nameof(bloggingService));
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<ProductCategory>> GetCategoryById(int id)
        {
            (bool isSuccess, Employee employee) = await this.employeeService.TryGetEmployeeIdAsync(id);
            if (isSuccess)
            {
                return new ObjectResult(employee);
            }

            return new NotFoundResult();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async IAsyncEnumerable<Employee> GetEmployee(int offset, int limit)
        {
            await foreach (var employee in this.employeeService
                               .GetEmployeesAsync(offset, limit))
            {
                yield return employee;
            }
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            var categoryId = await this.employeeService
                .CreateEmployeeAsync(employee);
            employee.Id = categoryId;
            return this.CreatedAtAction(nameof(this.GetCategoryById), new
            {
                id = employee.Id
            }, employee);
        }

        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await this.employeeService
                .DeleteEmployeeAsync(id);
            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return this.BadRequest();
            }

            var result = await this.employeeService
                .UpdateEmployeeAsync(id, employee);
            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
