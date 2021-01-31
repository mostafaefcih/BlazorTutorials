using EmployeeManagement.Api.Models.Filter;
using EmployeeManagement.Api.Models.Wrappers;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Sort;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            return await httpClient.PostJsonAsync<Employee>("api/Employees", employee);

        }

        public async Task DeleteEmployee(int employeeId)
        {
          await  httpClient.DeleteAsync($"api/employees/{employeeId}");
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await httpClient.GetJsonAsync<Employee>($"api/employees/{id}");
        }
        
        public async Task<PagedResponse<List<Employee>>> GetAllEmployees(PaginationFilter filter, SortCriteria criteria) 
        {
            var url = $"api/Employees/getall?PageNumber={filter.PageNumber}&PageSize={filter.PageSize}&sortField={criteria.SortField}&sortOrder={criteria.SortOrder} ";
            var result = await httpClient.GetJsonAsync<PagedResponse<List<Employee>>>(url);
            return result;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var url = $"api/Employees";
          var result=   await httpClient.GetJsonAsync<Employee[]>(url);
            return result;
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            return await httpClient.PutJsonAsync<Employee>("api/Employees", updatedEmployee);
        }

       
    }
}
