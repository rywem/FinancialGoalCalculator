using FinancialGoalCalculator.Web.Entities.Interfaces;
using FinancialGoalCalculator.Web.Enum;
using System.ComponentModel.DataAnnotations;

namespace FinancialGoalCalculator.Web.Entities.Accounts
{
    public class Account : IAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }        
        public AccountType AccountType { get; set; }
        public bool Closed { get; set; } = false;
        public ICollection<Balance> Balances { get; set; }
    }
}
