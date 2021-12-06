using ExpenseManager.Models.Authentication.Models;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICompanyService _companyService;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration, 
            ICompanyService companyService, 
            SignInManager<ApplicationUser> signInManager, 
            IAccountService accountService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _companyService = companyService;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    return Ok(new
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        CompanyId = user.CompanyId,
                        ApiToken = await CreateTokenAsync(user)
                    });
                }

                return StatusCode(400, "Invalid email or password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    	[HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var company = new Company() { Name = model.CompanyName };
                var createdCompany = await _companyService.CreateAsync(company);

                var user = new ApplicationUser()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CompanyId = createdCompany.Id,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);

                    return Ok(new
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        CompanyId = user.CompanyId,
                        ApiToken = await CreateTokenAsync(user)
                    });
                }
                return StatusCode(500, "User registration fail!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "User registration fail!");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost]
        [Route("emailExists")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailExists(string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);  
            if (user != null)
                return Ok(true);

            return Ok(false);
        }

        [HttpGet("getApplicationContext")]
        public async Task<IActionResult> GetApplicationContext() 
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var company = await _companyService.GetAsync(loggedInUser.CompanyId);

            return Ok(new {
                user = new {
                  fullName = loggedInUser.FullName,
                  userId = loggedInUser.Id,
                  email = loggedInUser.Email,
                  phoneNumber = loggedInUser.PhoneNumber,
                },
                company = company
            });
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
		{
			var userRoles = await _userManager.GetRolesAsync(user);
			var authClaims = new List<Claim> {
                new Claim(ClaimTypes.PrimarySid, user.Id),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};
			foreach (var userRole in userRoles) {
				authClaims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));

			var securityToken = new JwtSecurityToken(
				issuer: _configuration["JwtSettings:ValidIssuer"],
				audience: _configuration["JwtSettings:ValidAudience"],
				expires: DateTime.Now.AddDays(1),
				claims: authClaims,  
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
			);

			var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
			return token;
		}

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.PrimarySid);
            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            return loggedInUser;
        }

        // send invitation to user to join company by email
        [HttpPost]
        [Route("send-invitation")]
        public async Task<IActionResult> SendInvitation(string email)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, "Email field is not valid");

            if (await _accountService.IsMemberAlready(email))
                return StatusCode(400, "Member already exist");

            try
            {
                await _accountService.SendInvitation(email);
                return Ok("Invitation successfully sent");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}