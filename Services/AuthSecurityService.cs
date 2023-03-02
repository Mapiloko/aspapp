using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using aspapp.DTO.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static aspapp.DTO.Accounts.AuthResponseStatus;
using static aspapp.DTO.Accounts.UserModels;

namespace aspapp.Services
{
    public class AuthSecurityService
    {
        IConfiguration configuration;
        SignInManager<IdentityUser> signInManager;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public AuthSecurityService(IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(IdentityRole role)
        {
            bool isRoleCreated = false;
            var res = await roleManager.CreateAsync(role);
            if (res.Succeeded)
            {
                isRoleCreated = true;
            }
            return isRoleCreated;
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            List<Users> users = new List<Users>();
            users = (from u in await userManager.Users.ToListAsync()
                    select new Users()
                    {
                         Email = u.Email,
                         UserName = u.UserName
                    }).ToList();
            return users;
        }
        public async Task<bool> RegisterUserAsync(RegisterUser register)
        {
            bool IsCreated = false;
            
            var registerUser = new IdentityUser() { UserName = register.Email, Email = register.Email };
            var result = await userManager.CreateAsync(registerUser, register.Password);
            if (result.Succeeded)
            {
                IsCreated = true;
            }
            return IsCreated;
        }

        public async Task<bool> ChangeRole(UserRole userRole)
        {
            bool isRoleChanged = false;
            var user = await userManager.FindByEmailAsync(userRole.UserName);
            var roles = await userManager.GetRolesAsync(user);
            if(roles.Count != 0 && roles[0] != userRole.RoleName)
            {
                isRoleChanged = true;
                var result = await userManager.RemoveFromRoleAsync(user, roles[0]);
                var res =  await userManager.AddToRoleAsync(user, userRole.RoleName);              
            }
            return isRoleChanged;
        }

        public async Task<bool> UpdateUser(UserUpdate updateuser)
        {
            bool IsUserUpdated = false;
            // var user = await _userManager.FindByEmailAsync(employee_.Email);

            var user = await userManager.FindByEmailAsync(updateuser.UserName);
            if (user != null)
            {
                IsUserUpdated = true;
                user.Email = updateuser.NewUserName;
                user.UserName = updateuser.NewUserName;
                user.PhoneNumber = updateuser.PhoneNumber; 
                IdentityResult result = await userManager.UpdateAsync(user);
            }
            return IsUserUpdated;
        }

        /// Method to Assign Role to User
        public async Task<bool> AssignRoleToUserAsync(UserRole user)
        {
            bool isRoleAssigned = false;
            var role = roleManager.FindByNameAsync(user.RoleName).Result;
            var registeredUser = await userManager.FindByNameAsync(user.UserName);
            if (role != null)
            {
               var res =  await userManager.AddToRoleAsync(registeredUser, role.Name);
               if (res.Succeeded)
               {
                    isRoleAssigned = true;
               }
            }
            return isRoleAssigned;
        }

        /// Class to Authenticate User based on User Name
        public async Task<AuthStatus> AuthUserAsync(LoginUser inputModel)
        {
            string jwtToken = "";
            LoginStatus loginStatus;
            string roleName = "";
            var result = signInManager.PasswordSignInAsync(inputModel.UserName, 
            inputModel.Password, false, lockoutOnFailure: true).Result;
            if (result.Succeeded)
            {

                // Read the secret key and the expiration from the configuration 
                var secretKey = Convert.FromBase64String(configuration["JWTCoreSettings:SecretKey"]);
                var expiryTimeSpan = Convert.ToInt32(configuration["JWTCoreSettings:ExpiryInMinuts"]);
                // IdentityUser user = new IdentityUser(inputModel.UserName);
                var user = await userManager.FindByEmailAsync(inputModel.UserName);
                var role = await userManager.GetRolesAsync(user);
                // if user is not associated with role then log off
                if (role.Count == 0)
                {
                    await signInManager.SignOutAsync();
                    loginStatus =  LoginStatus.NoRoleToUser;
                }
                else
                {
                    //read the rolename
                    roleName = role[0];
                    // there is no third-party issuer
                    var securityTokenDescription = new SecurityTokenDescriptor()
                    {
                        Issuer = null,
                        Audience = null,
                        Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("userid",user.Id.ToString()),
                        new Claim("role",role[0])
                    }),
                        Expires = DateTime.UtcNow.AddYears(1),
                        IssuedAt = DateTime.UtcNow,
                        NotBefore = DateTime.UtcNow,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    // Now generate token using JwtSecurityTokenHandler
                    var jwtHandler = new JwtSecurityTokenHandler();
                    var jwToken = jwtHandler.CreateJwtSecurityToken(securityTokenDescription);
                    jwtToken = jwtHandler.WriteToken(jwToken);
                    loginStatus = LoginStatus.LoginSuccessful;
                }
            }
            else
            {
                loginStatus = LoginStatus.LoginFailed;
            }
            var authResponse = new AuthStatus()
            {
                 LoginStatus = loginStatus,
                 Token = jwtToken,
                 Role = roleName
            };
            return authResponse;
        }

        /// Thie method willaccept the token as inout parameter and wil receive token from it

        public async Task<string> GetUserFromTokenAsync(string token)
        {
            string userName = "";
            var jwtHandler = new JwtSecurityTokenHandler();
            // read the token values
            var jwtSecurityToken = jwtHandler.ReadJwtToken(token);
            // read claims
            var claims = jwtSecurityToken.Claims;
            // read first claim
            var userIdClaim = claims.First();
            // read the user Id
            var userId = userIdClaim.Value;
            // get the username from the userid
            var identityUser = await userManager.FindByIdAsync(userId);
            userName = identityUser.UserName;
            return userName;
        }

        public string GetRoleFormToken(string token)
        {
            string roleName = "";
            var jwtHandler = new JwtSecurityTokenHandler();
            // read the token values
            var jwtSecurityToken = jwtHandler.ReadJwtToken(token);
            // read claims
            var claims = jwtSecurityToken.Claims;
            // read first two claim
            var roleClaim = claims.Take(2);
            // read the role
            var roleRecord = roleClaim.Last();
            // read the role name
            roleName = roleRecord.Value;
            return roleName;
        }
    }
}