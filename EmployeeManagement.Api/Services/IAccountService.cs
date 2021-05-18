using EmployeeManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Services
{
    public interface IAccountService
    {
        Task<AuthModel> RegisterAsync(RegisterationModel model);
        Task<AuthModel> AuthenticateAsync(AuthenticationRequest model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
