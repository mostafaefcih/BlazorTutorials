using EmployeeManagement.Api.Helpers;
using EmployeeManagement.Api.Models.Filter;
using EmployeeManagement.Api.Models.Wrappers;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Filter;
using EmployeeManagement.Models.Sort;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<PagedResponse<List<Employee>>> GetAll(PaginationFilter filter, SortCriteria criteria)
        {
             
            var result = await appDbContext.Employees
                        .OrderByDynamic(criteria.SortField, criteria.SortOrder.ToUpper())
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
            var totalRecords = await appDbContext.Employees.CountAsync();
            var pagedResponse = new PagedResponse<List<Employee>>(result, totalRecords, filter.PageNumber, filter.PageSize);
            return pagedResponse;

        }
        //public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        public async Task<IEnumerable<Employee>> Search(EmployeeFilter filter)
        {
            int gnd = (int)filter.Gender;
            IQueryable<Employee> query = appDbContext.Employees;
            //var x = string.IsNullOrEmpty(filter.Name) ? DBNull.Value : filter.Name;
            SqlParameter name = new SqlParameter("@Name", string.IsNullOrEmpty(filter.Name) ? (object)DBNull.Value : filter.Name); 
            SqlParameter Email = new SqlParameter("@Email", string.IsNullOrEmpty(filter.Email) ? (object)DBNull.Value : filter.Email);
            SqlParameter Gender = new SqlParameter("@Gender", (int)filter.Gender==-1 ? (object)DBNull.Value : filter.Gender);
            SqlParameter DepartmentId = new SqlParameter("@DepartmentId", (int)filter.DepartmentId==-1 ? (object)DBNull.Value : filter.DepartmentId);
           
            var result = appDbContext.Employees.
                FromSqlRaw<Employee>("spSearchEmployees @Name, @Email, @Gender ,@DepartmentId"
                , name,Email ,Gender, DepartmentId).ToListAsync();    
            return await result;

            //if (!string.IsNullOrEmpty(name))
            //{
            //    query = query.Where(e => e.FirstName.Contains(name)
            //                || e.LastName.Contains(name));
            //}

            //if (gender != null)
            //{
            //    query = query.Where(e => e.Gender == gender);
            //}

            //return await query.ToListAsync();
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await appDbContext.Employees.Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBrith = employee.DateOfBrith;
                result.Gender = employee.Gender;
                result.DepartmentId = employee.DepartmentId;
                result.PhotoPath = employee.PhotoPath;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var result = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result != null)
            {
                appDbContext.Employees.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }


    }
}
