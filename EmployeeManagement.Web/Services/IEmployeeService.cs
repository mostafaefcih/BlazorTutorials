using EmployeeManagement.Api.Models.Filter;
using EmployeeManagement.Api.Models.Wrappers;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Filter;
using EmployeeManagement.Models.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services
{
  public  interface IEmployeeService
    {
        Task<PagedResponse<List<Employee>>> GetAllEmployees(PaginationFilter filter,SortCriteria criteria);
        Task<IEnumerable<Employee>> Search(EmployeeFilter filter);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<Employee> UpdateEmployee(Employee updatedEmployee);
        Task<Employee> CreateEmployee(Employee employee);
        Task DeleteEmployee(int  employeeId);

    }
}
