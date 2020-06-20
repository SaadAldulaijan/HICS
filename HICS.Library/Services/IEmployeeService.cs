using HICS.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public interface IEmployeeService
    {
        Task Delete(int id);
        Task<List<Employee>> Get();
        Task<Employee> GetById(int id);
        Task Post(Employee employee);
        Task Update(int id, Employee employee);
    }
}