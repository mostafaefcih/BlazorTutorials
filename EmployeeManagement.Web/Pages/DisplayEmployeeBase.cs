using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
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
    }
}
