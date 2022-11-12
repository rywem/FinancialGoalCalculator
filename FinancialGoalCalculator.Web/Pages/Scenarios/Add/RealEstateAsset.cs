using FinancialGoalCalculator.Web.Entities.Cases;

namespace FinancialGoalCalculator.Web.Pages.Scenarios.Add
{
    public partial class RealEstateAsset
    {
        [Parameter] public int ScenarioId { get; set; }
        [Parameter] public int AccountId { get; set; }
        [Inject] private RealEstateAssetCaseService RealEstateAssetCaseService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private RealEstateAssetCase _realEstateAssetCase;

        protected override async Task OnInitializedAsync()
        {
            _realEstateAssetCase = await RealEstateAssetCaseService.GetFirstOrNewAsync(ScenarioId, AccountId);
        }

        private async Task HandleSubmit()
        {
            if(_realEstateAssetCase.Id == 0)            
                await RealEstateAssetCaseService.CreateAsync(_realEstateAssetCase);            
            else 
                await RealEstateAssetCaseService.UpdateAsync(_realEstateAssetCase);
            NavigationManager.NavigateTo($"scenarios/build/{_realEstateAssetCase.ScenarioId}");

        }
    }
}
