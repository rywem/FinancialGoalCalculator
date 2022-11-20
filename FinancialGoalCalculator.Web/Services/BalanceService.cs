using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities;
using FinancialGoalCalculator.Web.Entities.Accounts;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class BalanceService
    {
        private readonly ApplicationDbContext _context;

        public BalanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBalanceAsync(Balance newBalance)
        {
            if(newBalance != null)
            {
                _context.Balance.Add(newBalance);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Balance> GetLastBalanceByAccount(Account account)
        {
            return await _context.Balance
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync(x => x.AccountId == account.Id);
        }
    }
}
