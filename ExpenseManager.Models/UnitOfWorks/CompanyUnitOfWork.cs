using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Repositories;

namespace ExpenseManager.Models.UnitOfWorks
{
    public class CompanyUnitOfWork: UnitOfWorkBase<DataContext>, ICompanyUnitOfWork
    {
        public CompanyUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            CompanyRepository = new CompanyRepository(_dbContext);
        }

        public ICompanyRepository CompanyRepository { get; private set; }
    }
}