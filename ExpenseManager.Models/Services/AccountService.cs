using ExpenseManager.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly IEmailSender _emailSender;

        public AccountService(IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            ICompanyService companyService, 
            IEmailSender emailSender)
        {
            _HttpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _companyService = companyService;
            _emailSender = emailSender;
        }

        public async Task<bool> IsMemberAlready(string email)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;

            if (user.CompanyId == loggedInUser.CompanyId)
                return true;

            return false;
        }
        public async Task SendInvitation(string email)
        {
            var loggedInUser = await GetLoggedInUserAsync();
            var company = await _companyService.GetAsync(loggedInUser.CompanyId);
            if (company is null)
                throw new Exception("Company not found");

            var appUser = new ApplicationUser() 
            { 
                FullName = email,
                Email = email, 
                UserName = email, 
                CompanyId = loggedInUser.CompanyId,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            string invitedUserPassword = "Exp$123" + Guid.NewGuid().ToString().Substring(25);

            var result = await _userManager.CreateAsync(appUser, invitedUserPassword);
            if (result.Succeeded)
            {
                var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(await _userManager.GenerateEmailConfirmationTokenAsync(appUser)));
                var link = $"{GetSiteBaseUrl()}/confirm-email?email={email}&token={token}";
                var htmlEmailBody = $@"<div>
                                           <p> <span style='color: blue'>{loggedInUser.FullName}</span> invited you to join {company.Name.ToUpper()}<p> <br/>
                                           <p>Use password: {invitedUserPassword}  to login</p> <br/>
                                           <a href='{link}'>Confirm email</a>
                                       </div>";

                var emailMessage = new EmailMessage()
                {
                    ToAddresses = new List<string> { email },
                    Subject = "Confirm your email",
                    Content = htmlEmailBody
                };
                await _emailSender.SendAsync(emailMessage);
            }
        }


        private string GetSiteBaseUrl()
        {
            HttpRequest request = _HttpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }

        private async Task<ApplicationUser> GetLoggedInUserAsync()
        {
            var loggedInUserId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid);
            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            return loggedInUser;
        }

        public async Task<bool> EmailConfirmed(string email, string token)
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
            var emailConfirmationStatus = await _userManager.ConfirmEmailAsync(appUser, token);
            if (appUser == null && !emailConfirmationStatus.Succeeded)
                throw new Exception("Invalid Token");

            return true;
        }
    }
}
