using FinancialGoalCalculator.Web.Entities.Cases;
using FinancialGoalCalculator.Web.Enum;

namespace FinancialGoalCalculator.Web.Pages.Scenarios.Add
{
    public partial class Retirement
    {
        [Parameter] public int ScenarioId { get; set; }
        [Parameter] public int AccountId { get; set; }
        [Inject] private GeneralAssetCaseService GeneralAssetCaseService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private GeneralAssetCase _generalAssetCase;

        protected override async Task OnInitializedAsync()
        {
            _generalAssetCase = await GeneralAssetCaseService.GetFirstOrNewAsync(ScenarioId, AccountId);
        }

        private async Task HandleSubmit()
        {
            if (_generalAssetCase.Id == 0)
                await GeneralAssetCaseService.CreateAsync(_generalAssetCase);
            else
                await GeneralAssetCaseService.UpdateAsync(_generalAssetCase);
            NavigationManager.NavigateTo($"scenarios/build/{_generalAssetCase.ScenarioId}");
        }
        private void OnPaymentIntervalValueChanged(PaymentInterval value)
        {
            _generalAssetCase.PaymentInterval = value;
        }
    }
}
