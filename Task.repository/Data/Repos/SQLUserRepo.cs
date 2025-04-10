using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task.application.Dtos;
using Task.application.Response;
using Task.core.Models;

namespace Task.repository.Data.Repos
{
    public class SQLUserRepo : IUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        public SQLUserRepo(UserManager<AppUser> userManager,IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterAsync(RegisterDto user)
        {
            var existingUser = await _userManager.FindByNameAsync(user.Email);
            if (existingUser != null)
            {

                return false;
            }
            AppUser appUser = new AppUser();
            appUser.UserName = user.UserName;
            appUser.Email = user.Email;

            var result = await _userManager.CreateAsync(appUser, user.Password);
            if (result.Succeeded)
            {
                string role = "User";

                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {

                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleResult.Succeeded)
                    {
                        return false;
                    }
                }
                var addToRoleResult = await _userManager.AddToRoleAsync(appUser, role);
                if (!addToRoleResult.Succeeded)
                {
                    return false;
                }

                return true;

            }
            else
            {
                //foreach (var error in result.Errors)
                //{
                    
                //}
                return false;
            }

        }

        public async Task<AuthResult> LoginAsync(LoginDto user)
        {
            AuthResult authResult = new AuthResult();
            if (string.IsNullOrEmpty(user.Email))
            {
                authResult.Success = false;
                authResult.Message = "Email is required";
                authResult.Token = string.Empty;
                return authResult;
            }
            AppUser? appUserFromDb = await _userManager.FindByEmailAsync(user.Email);
            if (appUserFromDb == null)
            {
                authResult.Success = false;
                authResult.Message = "Invalid credentials";
                authResult.Token = string.Empty;
                return authResult;
            }
            bool found = await _userManager.CheckPasswordAsync(appUserFromDb, user.Password);
            if (found)
            {
                authResult.Success = true;
                authResult.Message = "Login successful";
                
                var userRoles= await _userManager.GetRolesAsync(appUserFromDb);
                List<Claim> claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, appUserFromDb.Id),
                    new Claim(ClaimTypes.Email, appUserFromDb.Email),
                    new Claim(ClaimTypes.Name, appUserFromDb.UserName),
                };
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }


                SigningCredentials signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisISaVeryLongSecreteKeyComprisedof64CharsAndsomeotherstuff")),
                    SecurityAlgorithms.HmacSha256Signature
                );
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: "https://localhost:7099/" ,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signingCredentials
                );

                authResult.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return authResult;
            }
            else
            {
                authResult.Success = false;
                authResult.Message = "Invalid credentials";
                authResult.Token = string.Empty;
                return authResult;
            }
        }
    }
}
