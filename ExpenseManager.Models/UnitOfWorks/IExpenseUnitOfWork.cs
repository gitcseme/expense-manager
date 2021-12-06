using ExpenseManager.Models.Repositories;
using ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.Models.UnitOfWorks
{
    public interface IExpenseUnitOfWork : IUnitOfWorkBase
    {
        IExpenseRepository ExpenseRepository { get; }
    }
}
