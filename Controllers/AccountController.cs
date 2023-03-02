using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static aspapp.DTO.Accounts.AuthResponseStatus;
using static aspapp.DTO.Accounts.UserModels;

namespace aspapp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthSecurityService authService;
        public AccountController(AuthSecurityService service)
        {
            authService = service;
        }

        [Authorize(Policy = "AdminPolicy")]
        [Route("get-users")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            ResponseStatus response;
            try
            {
                var users = await authService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message,"", "");
                return BadRequest(response);
            }
        }
        [Authorize(Policy = "AdminPolicy")]
        [Route("create-role")]
        [HttpPost]
        public async Task<IActionResult> PostRoleAsync(ApplicationRole role)
        {
            ResponseStatus response;
            try
            {
                IdentityRole roleInfo = new IdentityRole()
                {
                     Name = role.Name,
                     NormalizedName = role.NormalizedName
                };
                var res  = await authService.CreateRoleAsync(roleInfo);
                if (!res)
                {
                    response = SetResponse(500, "Role Registration Failed","", "");
                    return StatusCode(500, response);
                }
                response = SetResponse(200, $"{role.Name} is Created sussessfully","", "");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message,"", "");
                return BadRequest(response);
            }
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync(RegisterUser user)
        {
            ResponseStatus response;
            try
            {
                var res = await authService.RegisterUserAsync(user);
                if (!res)
                {
                    response = SetResponse(500, "User Registration Failed","","");
                    return StatusCode(500, response);
                }
                response = SetResponse(200, $"User {user.Email} is Created sussessfully","","");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [Route("activate-user")]
        [HttpPut]
        public async Task<IActionResult> ActivateUserAsync(UserRole user)
        {
            ResponseStatus response;
            try
            {
                var res = await authService.AssignRoleToUserAsync(user);
                if (!res)
                {
                    response = SetResponse(500, "Role is not assigned to user","","");
                    return StatusCode(500, response);
                }
                response = SetResponse(200, "Role is sussessfully assigned to user","","");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
        }


        [Authorize(Policy = "AdminPolicy")]
        [Route("changerole")]
        [HttpPut]

        public async Task<IActionResult> ChangeRole(UserRole user)
        {
            ResponseStatus response;
            try
            {
                var res = await authService.ChangeRole(user);
                if (!res)
                {
                    response = SetResponse(500, "Role Changed","","");
                    return StatusCode(500, response);
                }
                response = SetResponse(200, "Role is sussessfully assigned to user","","");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
        }

        [Authorize(Policy = "AdminManagerEmployeePolicy")]
        [Route("put/update/user")]
        [HttpPut]

        public async Task<IActionResult> UpdateUser(UserUpdate updateUser)
        {
            ResponseStatus response;
            try
            {
                var res = await authService.UpdateUser(updateUser);
                if (!res)
                {
                    response = SetResponse(500, "Role Changed","","");
                    return StatusCode(500, response);
                }
                response = SetResponse(200, "UserUpdated","","");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
        }

        [Route("post/login")]
        [HttpPost]
        public async Task<IActionResult> AuthUserAsync(LoginUser user)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var res = await authService.AuthUserAsync(user);
                if (res.LoginStatus == LoginStatus.LoginFailed)
                {
                    response = SetResponse(401,"UserName or Password is not found", "","");
                    return Unauthorized(response);
                }
                if (res.LoginStatus == LoginStatus.NoRoleToUser)
                {
                    response =  SetResponse(401, "User is not activated with role. Please contact admin on mahesh@myapp.com","","");
                    return Unauthorized(response);
                }
                if (res.LoginStatus == LoginStatus.LoginSuccessful)
                {
                    response = SetResponse(200, "Login Sussessful", res.Token, res.Role);
                    response.UserName = user.UserName;
                    return Ok(response);
                }
                else
                {
                    response = SetResponse(500, "Internal Server Error Occured","","");
                    return StatusCode(500,response);
                }
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
        }

        /// Method to Set the Response
        private ResponseStatus SetResponse(int code, string message, string token, string role)
        {
            ResponseStatus response = new ResponseStatus()
            { 
               StatusCode = code,
               Message = message,
               Token = token,
               Role = role
            };
            return response;
        }
    }
}