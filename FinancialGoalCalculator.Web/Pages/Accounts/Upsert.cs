using FinancialGoalCalculator.Web.Entities.Accounts;
using FinancialGoalCalculator.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FinancialGoalCalculator.Web.Pages.Accounts
{
    public partial class Upsert
    {
        [Parameter] public int? Id { get; set; }

        private Account _Account;
        [Inject] private AccountService AccountService { get; set; }

        protected override Task OnInitialized()
        {
            return base.OnInitializedAsync();
        }
    }
}
