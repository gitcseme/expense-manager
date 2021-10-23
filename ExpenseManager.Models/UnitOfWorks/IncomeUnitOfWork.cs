using ExpenseManager.Repositories;
using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.UnitOfWorks
{
    public class IncomeUnitOfWork : UnitOfWorkBase<DataContext>, IIncomeUnitOfWork
    {
        public IncomeUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            IncomeRepository = new IncomeRepository(_dbContext);
        }

        public IIncomeRepository IncomeRepository { get; private set; }
    }
}
