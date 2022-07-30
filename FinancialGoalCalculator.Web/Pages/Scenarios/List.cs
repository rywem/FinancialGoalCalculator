namespace FinancialGoalCalculator.Web.Pages.Scenarios
{
    public partial class List
    {
        [Inject] private ScenarioService ScenarioService { get; set; }
        List<Scenario> _scenarios { get; set; }
        bool _loading { get; set; } = false;

        bool _showCreate = false;
        private Scenario _newScenario { get; set; } = new Scenario();
        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _loading = true;
            _scenarios = await ScenarioService.GetScenariosAsync();
            _loading = false;
        }

        private void showHideCreate()
        {
            _showCreate = !_showCreate;
        }

        private async Task HandleSubmit()
        {
            _loading = true;
            await ScenarioService.AddScenarioAsync(_newScenario);
            _newScenario = new Scenario();
            showHideCreate();
            await LoadAsync();
            _loading = false;

        }
    }
}
