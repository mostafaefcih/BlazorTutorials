using EmployeeManagement.Api.Models.Filter;
using EmployeeManagement.Api.Models.Wrappers;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Filter;
using EmployeeManagement.Models.Sort;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public EmployeeFilter employeeSearch { get; set; } = new EmployeeFilter();
        public IEnumerable<Employee> SearchedEmployees { get; set; } = new List<Employee>();
        public Department FilterdDepartment { get; set; } 
        public bool ShowFooter { get; set; } = true;
        protected int SelectedEmployeesCount { get; set; } = 0;
        public PagedResponse<List<Employee>> paginatedList { get; set; }
        PaginationFilter filter = new PaginationFilter() { PageNumber = 1, PageSize = 10 };

        SortCriteria sort = new SortCriteria() { SortField = "FirstName", SortOrder = "Asc" };
        protected async Task Sort(string sortField)
        {
            if (sortField.Equals(sort.SortField))
            {
                sort.SortOrder = sort.SortOrder.Equals("Asc") ? "Desc" : "Asc";
            }
            else
            {
                sort.SortField = sortField;
                sort.SortOrder = "Asc";
            }
            await LoadData();
            //paginatedList = await service.GetPagedResult(pageNumber, currentSortField, currentSortOrder);
            //toDoList = paginatedList.Items;
        }
        private async Task LoadData()
        {
             paginatedList = (await EmployeeService.GetAllEmployees(filter, sort));
            Employees = paginatedList.Data;
            SearchedEmployees = Employees;
            //paginatedList = await service.GetPagedResult(pageNumber, currentSortField, currentSortOrder);
            //toDoList = paginatedList.Items;
            //Employees = (await EmployeeService.GetAllEmployees(filter, sort)).Data;
        }
        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetDepartments();
            await LoadData();
            //Employees = (await EmployeeService.GetEmployees()).ToList();
            //return base.OnInitializedAsync();
        }
        public Gender selectedGender { get; set; } = Gender.Female;
        public string SelectedName { get; set; }
        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }

        protected async Task EmployeeDeleted()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
            SearchedEmployees = Employees;
        }
        public void SearchByGender(ChangeEventArgs e)
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
        public async void PageIndexChanged(int newPageNumber)
        {
            filter.PageNumber = newPageNumber;
            await LoadData();
            StateHasChanged();
            //var paginatedList = (await EmployeeService.GetAllEmployees(filter));
            //   Employees = paginatedList.Data;
            //   SearchedEmployees = Employees;
            //paginatedList = await service.GetPagedResult(pageNumber, currentSortField, currentSortOrder);
            //toDoList = paginatedList.Items;
        }
        protected string SortIndicator(string sortField)
        {
            if (sortField.Equals(sort.SortField))
            {
                return sort.SortOrder.Equals("Asc") ? "fa fa-sort-asc" : "fa fa-sort-desc";
            }
            return string.Empty;
        }
        protected async Task Search() {
          var result= await EmployeeService.Search(employeeSearch);
            if(employeeSearch.DepartmentId !=null && employeeSearch.DepartmentId != default)
            {
                 FilterdDepartment =await DepartmentService.GetDepartment(employeeSearch.DepartmentId.Value);
                
            }
            SearchedEmployees = result;

        }
    }
}
