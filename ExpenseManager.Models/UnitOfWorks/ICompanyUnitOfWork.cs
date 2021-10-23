using ExpenseManager.Repositories;
using ExpenseManager.Models.Repositories;

namespace ExpenseManager.Models.UnitOfWorks
{
    public interface ICompanyUnitOfWork : IUnitOfWorkBase
    {
        ICompanyRepository CompanyRepository { get; }
    }
}