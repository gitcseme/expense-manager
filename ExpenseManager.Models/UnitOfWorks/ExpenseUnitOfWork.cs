using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Repositories;
using ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.UnitOfWorks
{
    public class ExpenseUnitOfWork : UnitOfWorkBase<DataContext>, IExpenseUnitOfWork
    {
        public ExpenseUnitOfWork(string connectionString, string migrationAssemblyName) 
            : base(connectionString, migrationAssemblyName)
        {
            ExpenseRepository = new ExpenseRepository(_dbContext);
        }

        public IExpenseRepository ExpenseRepository { get; private set; }
    }
}
