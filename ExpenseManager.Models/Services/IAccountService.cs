using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Services
{
    public interface IAccountService
    {
        Task<bool> IsMemberAlready(string email);
        Task SendInvitation(string email);
        Task<bool> EmailConfirmed(string email, string token);
    }
}
