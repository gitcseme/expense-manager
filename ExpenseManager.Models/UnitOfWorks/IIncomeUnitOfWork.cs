using ExpenseManager.Repositories;
using ExpenseManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.UnitOfWorks
{
    public interface IIncomeUnitOfWork : IUnitOfWorkBase
    {
        IIncomeRepository IncomeRepository { get; }
    }
}
