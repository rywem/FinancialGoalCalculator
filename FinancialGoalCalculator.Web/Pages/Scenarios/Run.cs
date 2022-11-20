using FinancialGoalCalculator.Web.Models.Scenarios;

namespace FinancialGoalCalculator.Web.Pages.Scenarios
{
    public partial class Run
    {
        [Parameter] public int ScenarioId { get; set; }
        public int _years { get; set; } = 10;
        private bool showScenario = false;
        [Inject] ScenarioService ScenarioService { get; set; }

        private List<YearAggregateModel> _yearAggregateList;
        private async Task RunScenario()
        {
            _yearAggregateList = await ScenarioService.GenerateScenario(ScenarioId, _years);
        }
    }
}
