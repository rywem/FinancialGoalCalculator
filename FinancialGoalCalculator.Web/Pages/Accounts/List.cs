using FinancialGoalCalculator.Web.Entities.Accounts;
using FinancialGoalCalculator.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FinancialGoalCalculator.Web.Pages.Accounts
{
    public partial class List
    {
        [Inject] private AccountService AccountService { get; set; }

        private List<Account> _accounts;
        private List<Account> _accountsFiltered;
        private FormModel _model { get; set; } = new FormModel();
        protected override void OnInitialized()
        {
            _accounts = AccountService.GetAccounts();
            _accountsFiltered = _accounts;
        }

        private void HideClosedCheckboxChanged(ChangeEventArgs e)
        {
            bool currentValue = (bool)e.Value;

            if (currentValue == true)
                _accountsFiltered = _accounts.Where(x => x.Closed == false).ToList();
            else
                _accountsFiltered = _accounts;

            StateHasChanged();
        }
        private class FormModel
        {
            public bool HideClosed { get; set; } = false;
        }
    }
}
