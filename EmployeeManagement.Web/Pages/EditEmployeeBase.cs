using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase:ComponentBase
    {
        [Inject]
        IEmployeeService employeeService { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Parameter]
        public string   Id { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployee { get; set; } = new EditEmployeeModel();
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public string DepartmentId { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string PageHeaderText { get; set; }
        protected override async Task OnInitializedAsync()
        {
             int.TryParse(Id, out int  employeeId);

            if (employeeId !=0) {
                PageHeaderText = "Edit Employee";
                Employee = await employeeService.GetEmployee(employeeId);
            }
           else {
                PageHeaderText = "Create Employee";
                Employee = new Employee()
                {
                    DepartmentId = 1,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = ""
                };
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();
            DepartmentId = Employee.DepartmentId.ToString();
            Mapper.Map(Employee, EditEmployee);
        }

      protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployee, Employee);
            Employee result;
            if (Employee.EmployeeId != 0)
            {

            result =await employeeService.UpdateEmployee(Employee);

            }
            else
            {
                result = await employeeService.CreateEmployee(Employee);

            }
            
            if (result != null) {

                NavigationManager.NavigateTo("/");
            }

        }

        protected async Task Delete_Click() {
           await employeeService.DeleteEmployee(Employee.EmployeeId);
                NavigationManager.NavigateTo("/");

        }
    }
}
