namespace FinancialGoalCalculator.Web.Pages.Scenarios
{
    public partial class Build
    {
        [Parameter] public int ScenarioId { get; set; }
        [Inject] private AccountService _accountService { get; set; }
        private List<Account> _accounts;

        protected override async Task OnInitializedAsync()
        {
            _accounts = await _accountService.GetAccountsAsync();
            _accounts = _accounts.OrderBy(x => x.AccountType).ToList();
        }

        private string GetAddLink(Account account)
        {
            string result = string.Empty;

            if(account.AccountType == Enum.AccountType.Mortgage)
            {
                result = $"scenarios/add/mortgage/{ScenarioId}/{account.Id}";
            }
            if (account.AccountType == Enum.AccountType.RetirementAccount)
            {
                result = $"scenarios/add/retirement/{ScenarioId}/{account.Id}";
            }
            if (account.AccountType == Enum.AccountType.RealEstateAsset)
            {
                result = $"scenarios/add/realestateasset/{ScenarioId}/{account.Id}";
            }
            if (account.AccountType == Enum.AccountType.Asset)
            {
                result = $"scenarios/add/asset/{ScenarioId}/{account.Id}";
            }
            if (account.AccountType == Enum.AccountType.Debt)
            {
                result = $"scenarios/add/debt/{ScenarioId}/{account.Id}";
            }
            return result;
        }
    }
}
