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
        public IEnumerable<Employee> SearchedEmployees { get; set; } = new List<Employee>();
        public bool ShowFooter { get; set; } = true;
        protected int SelectedEmployeesCount { get; set; } = 0;
        protected override async Task OnInitializedAsync()
        {
             Employees = (await EmployeeService.GetEmployees()).ToList();
            SearchedEmployees = Employees;
            //return base.OnInitializedAsync();
        }
        public Gender selectedGender { get; set; } = Gender.Female;
        public string SelectedName { get; set; }
        protected void  EmployeeSelectionChanged(bool isSelected) {
            if (isSelected) {
                SelectedEmployeesCount++;
            } else {
                SelectedEmployeesCount--;
            }
        }

        protected async Task EmployeeDeleted() {
            Employees = (await EmployeeService.GetEmployees()).ToList();
            SearchedEmployees = Employees;
        }
        public void SearchByGender(ChangeEventArgs  e) 
        {
            int value = (int)Enum.Parse(typeof(Gender), e.Value.ToString());
            selectedGender = (Gender)value;
            if (e.Value.ToString() == "-1")
            {
                SearchedEmployees = Employees;
            }
            else
            {
                SearchedEmployees = Employees.Where(e => e.Gender == selectedGender);

            }
        }
    }
}
