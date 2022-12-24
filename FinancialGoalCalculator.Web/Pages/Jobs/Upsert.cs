namespace FinancialGoalCalculator.Web.Pages.Jobs
{
    public partial class Upsert
    {
        [Parameter] public int? Id { get; set; }
        [Inject] public JobService JobService { get; set; }
    }
}
