using Management.Model;
using Management.Common;

namespace Management.Repository.Common
{
    

    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(Filter filter, Paging paging, Sorting sorting);
        Task<Employee> GetByIdAsync(Guid id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }

}
