using FinancialGoalCalculator.Web.Enum;
using FinancialGoalCalculator.Web.Helpers;
using FinancialGoalCalculator.Web.Models;

namespace FinancialGoalCalculator.Web.Pages.Accounts
{
    public partial class List
    {
        [Inject] private AccountService AccountService { get; set; }
        [Inject] private BalanceService BalanceService { get; set; }

        private List<AccountRowModel> _accounts;
        private List<AccountRowModel> _accountsFiltered;
        private FormModel _model { get; set; } = new FormModel();
        private List<string> _errors = new List<string>();
        private bool _loading = false;
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            await Load();
            _loading = false;
        }

        private async Task Load()
        {
            _accounts = AccountRowModel.GetAccountRows(await AccountService.GetAccountsAsync()).ToList();
            _accountsFiltered = _accounts;
        }

        private decimal GetTotalCurrentBalance(AccountType accountType)
        {
            return _accountsFiltered.Where(x => x.Account.AccountType == accountType).Sum(x => x.Account.GetLastBalance());
        }
        private void HideClosedCheckboxChanged(ChangeEventArgs e)
        {
            bool currentValue = (bool)e.Value;

            if (currentValue == true)
                _accountsFiltered = _accounts.Where(x => x.Account.Closed == false).ToList();
            else
                _accountsFiltered = _accounts;

            StateHasChanged();
        }
        private void ShowHideBalanceHandler(AccountRowModel accountRow)
        {
            accountRow.ShowBalanceInsert = !accountRow.ShowBalanceInsert;
            accountRow.NewBalance = new Balance();
            accountRow.NewBalance.AccountId = accountRow.Account.Id;
            StateHasChanged();
        }
        private class FormModel
        {
            public bool HideClosed { get; set; } = false;
        }

        private async Task AddQuickBalanceAsync(Balance newBalance)
        {
            _loading = true;
            _errors = new List<string>();
            try
            {
                newBalance.Date = DateTime.Now;
                await BalanceService.AddBalanceAsync(newBalance);
            }
            catch(Exception ex)
            {
                _errors = ErrorHelper.GetErrors(ex);
            }
            await Load();
            _loading = false;
        }

        private string GetDetailsLink(AccountRowModel accountRow)
        {
            string result = string.Empty;
            if(accountRow != null)
            {
                if(accountRow.Account != null)
                {
                    if(accountRow.Account.AccountType == AccountType.Debt)
                    {
                        return $"/accounts/loans/details/{accountRow.Account.Id}";
                    }
                    else if (accountRow.Account.AccountType == AccountType.Asset)
                    {
                        return $"/accounts/asset/details/{accountRow.Account.Id}";
                    }
                }
            }

            return result;
        }
    }
}
