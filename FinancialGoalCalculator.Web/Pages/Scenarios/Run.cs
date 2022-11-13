namespace FinancialGoalCalculator.Web.Pages.Scenarios
{
    public partial class Run
    {
        [Parameter] public int ScenarioId { get; set; }
        public int _years { get; set; } = 10;
        private bool showScenario = false;
        private async Task RunScenario()
        {

        }
    }
}
