using Management.Model;
using Management.Service.Common;
using Management.Repository.Common;
using Management.Common;
namespace Management.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(Filter filter, Paging paging, Sorting sorting)
        {
            return await _employeeRepository.GetAllAsync(filter, paging, sorting);
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Guid id, Employee employee)
        {
            await _employeeRepository.UpdateAsync(id, employee);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
