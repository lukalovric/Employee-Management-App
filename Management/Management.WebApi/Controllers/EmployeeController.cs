using Management.Common;
using Management.Model;
using Management.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync(
            string searchSurname = "", string searchName = "",
            DateTime? startDate = null, DateTime? endDate = null,
            Guid? projectId = null, int rpp = 3, int pageNumber = 1,
            string orderBy = "CreatedAt", string sortOrder = "ASC")
        {
            try
            {
                var filter = new Filter { SearchSurname = searchSurname, SearchName = searchName, StartDate = startDate, EndDate = endDate, ProjectId = projectId };
                var paging = new Paging { RecordsPerPage = rpp, PageNumber = pageNumber };
                var sorting = new Sorting { OrderBy = orderBy, SortOrder = sortOrder };

                var employees = await _employeeService.GetAllEmployeesAsync(filter, paging, sorting);
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] Employee employee)
        {
            try
            {
                await _employeeService.CreateEmployeeAsync(employee);
                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(employee);
                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok($"Employee with id {id} deleted succesfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
