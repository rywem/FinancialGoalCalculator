using FinancialGoalCalculator.Web.Entities.Cases;

namespace FinancialGoalCalculator.Web.Pages.Scenarios.Add
{
    public partial class RealEstateAsset
    {
        [Parameter] public int ScenarioId { get; set; }
        [Parameter] public int AccountId { get; set; }
        [Inject] private RealEstateAssetCaseService RealEstateAssetCaseService { get; set; }
        private RealEstateAssetCase _realEstateAssetCase;

        protected override async Task OnInitializedAsync()
        {
            _realEstateAssetCase = await RealEstateAssetCaseService.GetFirstOrNewAsync(ScenarioId, AccountId);
        }
    }
}
