using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class DisplayEmployeeBase:ComponentBase
    {
        [Parameter]
        public bool ShowFooter { get; set; }
        [Parameter]
        public Employee Employee { get; set; }

        public bool IsSelected { get; set; }
        [Parameter]
      public  EventCallback<bool> OnEmployeeSelection { get; set; }
       public async Task CheckBoxChanged(ChangeEventArgs e) {
            await OnEmployeeSelection.InvokeAsync((bool)e.Value);
        }
        [Inject]
        IEmployeeService employeeService { get; set; }
        [Inject]
        public IJSRuntime js { get; set; }
        [Parameter]
        public EventCallback<int> onEmployeeDeleted { get; set; }
        protected async Task Delete_Click()
        {
            if(await js.InvokeAsync<bool>("confirm",$"Do you want to delete {Employee.LastName} ?"))
            {

            await employeeService.DeleteEmployee(Employee.EmployeeId);
            await onEmployeeDeleted.InvokeAsync(Employee.EmployeeId);
            }

        }
    }
}
