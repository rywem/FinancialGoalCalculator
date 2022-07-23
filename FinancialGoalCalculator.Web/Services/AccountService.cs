using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities.Accounts;

namespace FinancialGoalCalculator.Web.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAccount(Account account)
        {
            if (account != null)
            {
                _context.Account.Add(account);
                _context.SaveChanges();
            }
        }
    }
}
