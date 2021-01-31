using EmployeeManagement.Api.Models.Filter;
using EmployeeManagement.Api.Models.Wrappers;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Filter;
using EmployeeManagement.Models.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<PagedResponse<List<Employee>>> GetAll(PaginationFilter filter, SortCriteria sort);
        Task<IEnumerable<Employee>> Search(EmployeeFilter filter);
        //Task<IEnumerable<Employee>> Search(string name, Gender? gender);
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int employeeId);
    }
}
