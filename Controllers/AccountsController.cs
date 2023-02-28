using System.Text;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.DTO.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace aspapp.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController: ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

         [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create([FromBody] UserCredentials userCredetials)
        {
            var user  = new IdentityUser {UserName = userCredetials.Email, Email = userCredetials.Email};

            var result = await _userManager.CreateAsync(user, userCredetials.Password);

            if(result.Succeded)
            {
                return BuildToken(userCredetials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials userCredetials)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredetials.Email, userCredetials.Password, isPersistent: false
            ,lockoutOnFailure: false);

            if(result.Succeded)
            {
                return BuildToken(userCredetials);
            }
            else
            {
                return BadRequest("Incorrect login");
            }
        }

        private AuthenticationResponse BuildToken(UserCredentials userCredetials)
        {
            var claims  = new List<Claim>()
            {
                new Claim("email", userCredetials?.Email)
            };

            var key  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["keyjwt"]));
            var creds  = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);
            var token  = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}