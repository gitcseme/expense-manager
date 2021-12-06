using ExpenseManager.Repositories;
using ExpenseManager.Models.Data;
using System;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Seeds
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
