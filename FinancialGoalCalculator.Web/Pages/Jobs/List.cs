namespace FinancialGoalCalculator.Web.Pages.Jobs
{
    public partial class List
    {
        private List<Job> _jobs;
        [Inject] public JobService JobService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _jobs = await JobService.GetJobsAsync();
        }
    }
}
