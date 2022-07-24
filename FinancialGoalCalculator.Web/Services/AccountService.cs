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

        public void UpdateAccount(Account account)
        {
            if(account != null && account.Id > 0)
            {
                _context.Account.Update(account);
                _context.SaveChanges();
            }
        }

        public Account GetAccountById(int Id)
        {
            return _context.Account.FirstOrDefault(x => x.Id == Id);
        }
        public List<Account> GetAccounts()
        {
            return _context.Account.ToList();
        }
    }
}
