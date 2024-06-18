using AutoMapper;
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
        private readonly IMapper _mapper;


        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeGetRest>>> GetEmployeesAsync(
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
                var employeeGetRest = _mapper.Map<IEnumerable<EmployeeGetRest>>(employees);
                return Ok(employeeGetRest);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGetRest>> GetEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                var employeeGetRest = _mapper.Map<EmployeeGetRest>(employee);
                return Ok(employeeGetRest);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeePostRest employeePostRest)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeePostRest);
                await _employeeService.CreateEmployeeAsync(employee);
                var employeesPostRest = _mapper.Map<EmployeePostRest>(employee);
                return Ok(employeePostRest);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid id, [FromBody] EmployeePutRest employeePutRest)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeePutRest);
                await _employeeService.UpdateEmployeeAsync(id,employee);
                employeePutRest = _mapper.Map<EmployeePutRest>(employee);
                return Ok(employeePutRest);
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

public class EmployeeGetRest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Position { get; set; }
    public double? Salary { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class EmployeePostRest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Position { get; set; }
    public double? Salary { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class EmployeePutRest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Position { get; set; }
    public double? Salary { get; set; }
    public DateTime? CreatedAt { get; set; }
}



    public class MappingProfile : Profile
{
        
        public MappingProfile()
         {
            CreateMap<Employee, EmployeeGetRest>();
            CreateMap<EmployeePostRest, Employee>(); 
            CreateMap<EmployeePutRest, Employee>();
            CreateMap<Employee, EmployeePostRest>();
            CreateMap<Employee, EmployeePutRest>();
        }
}

