using FinancialGoalCalculator.Web.Entities.Cases;

namespace FinancialGoalCalculator.Web.Pages.Scenarios.Add
{
    public partial class RealEstateAsset
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
            _generalAssetCase.Payment = 0;
            _generalAssetCase.PaymentInterval = Enum.PaymentInterval.Monthly;
            if (_generalAssetCase.Id == 0)            
                await GeneralAssetCaseService.CreateAsync(_generalAssetCase);            
            else 
                await GeneralAssetCaseService.UpdateAsync(_generalAssetCase);
            NavigationManager.NavigateTo($"scenarios/build/{_generalAssetCase.ScenarioId}");

        }
    }
}
