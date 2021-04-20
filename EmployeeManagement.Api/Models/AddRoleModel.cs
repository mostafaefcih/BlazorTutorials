using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class AddRoleModel
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
