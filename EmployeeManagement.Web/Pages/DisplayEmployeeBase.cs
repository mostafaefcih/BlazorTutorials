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
    }
}
