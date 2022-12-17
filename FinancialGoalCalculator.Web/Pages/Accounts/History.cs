namespace FinancialGoalCalculator.Web.Pages.Accounts
{
    public partial class History
    {
        [Parameter] public int AccountId { get; set; }
        [Inject] private BalanceService BalanceService { get; set; }
        private List<Balance> _balances = new List<Balance>();
        
        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _balances = await BalanceService.GetBalancesByAccountId(AccountId);
        }
        private async Task DeleteBalanceHandler(int balanceId)
        {
            await BalanceService.DeleteBalanceAsync(balanceId);
            await LoadAsync();
        }
    }
}
