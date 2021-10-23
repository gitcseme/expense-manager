using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Entities;

namespace ExpenseManager.Models.Repositories
{
  public class CompanyRepository : RepositoryBase<Company, int, DataContext>, ICompanyRepository
  {
    public CompanyRepository(DataContext context) : base(context)
    {
    }
  }
}