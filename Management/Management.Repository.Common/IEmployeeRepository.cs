using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Model;

namespace Management.Repository.Common
{
    

    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(Guid id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Guid id);
    }

}
