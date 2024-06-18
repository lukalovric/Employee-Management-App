using Management.Model;
using Management.Common;

namespace Management.Service.Common
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(Filter filter, Paging paging, Sorting sorting);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Guid id, Employee employee);
        Task DeleteEmployeeAsync(Guid id);
    }   
}
