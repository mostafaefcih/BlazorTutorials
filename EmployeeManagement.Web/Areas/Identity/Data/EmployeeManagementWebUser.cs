using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Web.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the EmployeeManagementWebUser class
    public class EmployeeManagementWebUser : IdentityUser
    {
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
