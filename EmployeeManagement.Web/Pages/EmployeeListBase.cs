using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase:ComponentBase
    {
       [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
        public bool ShowFooter { get; set; }
        protected int SelectedEmployeesCount { get; set; } = 0;
        protected override async Task OnInitializedAsync()
        {
             Employees = (await EmployeeService.GetEmployees()).ToList();
            //return base.OnInitializedAsync();
        }
      protected void  EmployeeSelectionChanged(bool isSelected) {
            if (isSelected) {
                SelectedEmployeesCount++;
            } else {
                SelectedEmployeesCount--;
            }
        }

        protected async Task EmployeeDeleted() {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }
    }
}
