using FinancialGoalCalculator.Web.Entities.Accounts;
using FinancialGoalCalculator.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FinancialGoalCalculator.Web.Pages.Accounts
{
    public partial class Upsert
    {
        [Parameter] public int? Id { get; set; }
        [Inject] private AccountService AccountService { get; set; }

        private Account _Account;
        private List<string> _errors = new List<string>();

        protected override void OnInitialized()
        {
            _errors = new List<string>();
            if (Id == null)
            {
                _Account = new Account();
            }
            else
            {
                var acct = AccountService.GetAccountById((int)Id);
                if (acct != null)
                    _Account = acct;
                else
                {
                    _errors.Add("Account not found.");
                }
            }
        }
    }
}
