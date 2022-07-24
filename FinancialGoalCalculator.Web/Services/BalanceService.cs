using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities;
using FinancialGoalCalculator.Web.Entities.Accounts;

namespace FinancialGoalCalculator.Web.Services
{
    public class BalanceService
    {
        private readonly ApplicationDbContext _context;

        public BalanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBalance(Balance newBalance)
        {
            if(newBalance != null)
            {
                _context.Balance.Add(newBalance);
                _context.SaveChanges();
            }
        }
    }
}
