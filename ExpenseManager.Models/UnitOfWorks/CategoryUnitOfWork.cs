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
    public class CategoryUnitOfWork : UnitOfWorkBase<DataContext>, ICategoryUnitOfWork
    {
        public CategoryUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            CategoryRepository = new CategoryRepository(_dbContext);
        }

        public ICategoryRepository CategoryRepository { get; private set; }
    }
}
