using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities.Accounts;
using FinancialGoalCalculator.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAccountAsync(Account account)
        {
            if (account != null)
            {
                account.CreatedDate = DateTime.Now;
                _context.Account.Add(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAccountAsync(Account account)
        {
            if(account != null && account.Id > 0)
            {
                _context.Account.Update(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Account> GetAccountByIdAsync(int Id)
        {
            return await _context.Account.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _context.Account.Include("Balances").ToListAsync();
        }
    }
}
