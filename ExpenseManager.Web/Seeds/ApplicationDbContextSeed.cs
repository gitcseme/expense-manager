using ExpenseManager.Repositories;
using ExpenseManager.Web.Data;
using System;
using System.Threading.Tasks;

namespace ExpenseManager.Web.Seeds
{
    public class ApplicationDbContextSeed : DataSeed
	{
		public ApplicationDbContextSeed(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public override Task SeedAsync()
		{
			throw new NotImplementedException();
		}
	}
}
