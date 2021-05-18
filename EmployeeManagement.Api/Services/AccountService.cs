using EmployeeManagement.Api.Constants;
using EmployeeManagement.Api.Helpers;
using EmployeeManagement.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AccountService(UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager,
             RoleManager<IdentityRole> roleManager,

            IOptions<JWT> jwt)
        {
          _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwt = jwt.Value;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.RoleExistsAsync(model.Role);
            if (user == null || !role)
                return "invalid user or role";
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "user already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded ? string.Empty : "something  went wrong";

        }

        public async Task<AuthModel> AuthenticateAsync(AuthenticationRequest model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                  authModel. Message = $"No Accounts Registered with {model.Email}.";
                return authModel;
            }
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            //var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
            ////var result = await _userManager.CheckPasswordAsync(user, model.Password);
            //if (!result.Succeeded)
            //{
            // authModel.Message = $"Invalid Credentials for '{model.Email}'.";
            //    return authModel;
            //}



            authModel.isAuthenticated = true;

            var securityToken = await GenerateJwtToke(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.isAuthenticated = true;
            authModel.Roles = userRoles.ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            authModel.ExpiredOn = securityToken.ValidTo;
            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterationModel model)
        {
           if(await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel() {Message="email is already registered" };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel() { Message = "username is already registered" };

            var user = new ApplicationUser
            {
                UserName=model.UserName,
                FirstName=model.FirstName,
                LastName=model.LastName,
             Email=model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message=errors.Remove(errors.LastIndexOf(","))};
            }
            await _userManager.AddToRoleAsync(user, Roles.User.ToString());

            var securityToken =await  GenerateJwtToke(user);
            return new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                isAuthenticated = true,
                Roles = new List<string> { Roles.User.ToString() },
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                 ExpiredOn = securityToken.ValidTo,

        };
        }
   
    private async Task<JwtSecurityToken> GenerateJwtToke(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesClaims = new List<Claim>();
            foreach (var role in userRoles)
                rolesClaims.Add(new Claim("roles", role));
            var claims = new[] {

            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim("uid",user.Id),
            }
            .Union(userClaims)
            .Union(rolesClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                          issuer: _jwt.Issuer,
                          audience: _jwt.Audience,
                          claims: claims,
                          expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                          signingCredentials: signingCredentials);
            return jwtSecurityToken;


        }
    }
}
