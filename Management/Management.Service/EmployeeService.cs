using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Model;
using Management.Service.Common;
using Management.Repository.Common;

namespace Management.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public Employee GetEmployeeById(Guid id)
        {
            return _employeeRepository.GetById(id);
        }

        public void CreateEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _employeeRepository.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public void DeleteEmployee(Guid id)
        {
            _employeeRepository.Delete(id);
        }
    }
}
