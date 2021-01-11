using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Department GetDepartment(int departmentId)
        {
            return appDbContext.Departments
                .FirstOrDefault(d => d.DepartmentId == departmentId);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return appDbContext.Departments;
        }
    }
}
