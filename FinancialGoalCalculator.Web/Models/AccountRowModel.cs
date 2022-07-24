using FinancialGoalCalculator.Web.Entities;

namespace FinancialGoalCalculator.Web.Models
{
    public class AccountRowModel
    {
        public Account Account { get; set; }
        public bool ShowBalanceInsert { get; set; } = false;
        public Balance NewBalance { get; set; }

        public AccountRowModel(Account account)
        {
            Account = account;
            ShowBalanceInsert = false;
            NewBalance = new Balance();
        }

        public static IEnumerable<AccountRowModel> GetAccountRows(List<Account> accounts)
        {
            foreach (var account in accounts)
            {
                yield return new AccountRowModel(account);
            }
        }
    }
}
