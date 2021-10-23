using ExpenseManager.Repositories;

namespace ExpenseManager.Models.Entities
{
    public class Company : EntityBase<int>
    {
        public string Name { get; set; }
    }
}