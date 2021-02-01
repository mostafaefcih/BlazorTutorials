using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Models.Filter
{
    public class EmployeeFilter
    {
        public string Name { get; set; }
        public string Email { get; set; }
        //public DateTime DateOfBrith { get; set; }
        public Gender? Gender { get; set; }
        //public Department Department { get; set; }
        public int? DepartmentId { get; set; } 
        //public string PhotoPath { get; set; }
    }
}
