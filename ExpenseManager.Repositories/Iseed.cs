using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Repositories
{
    public interface Iseed
    {
        Task MigrateAsync();
        Task SeedAsync();
    }
}
