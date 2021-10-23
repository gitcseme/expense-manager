using ExpenseManager.Models.Contexts;
using ExpenseManager.Repositories;
using System;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Seeds
{
	public class DataContextSeed : DataSeed
	{
		public DataContextSeed(DataContext dbContext) : base(dbContext)
		{
		}

		public override Task SeedAsync() 
		{
			throw new NotImplementedException();
		}
	}
}