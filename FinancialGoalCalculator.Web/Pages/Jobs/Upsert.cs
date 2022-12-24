using FinancialGoalCalculator.Web.Helpers;

namespace FinancialGoalCalculator.Web.Pages.Jobs
{
    public partial class Upsert
    {
        [Parameter] public int? Id { get; set; }
        [Inject] private JobService JobService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private Job _job;
        bool isUpdate = false;
        private List<string> _errors = new List<string>();
        protected override async Task OnInitializedAsync()
        {
            if(Id != null)
            {
                _job = await JobService.GetJobAsync(Id.Value);
                if (_job != null)
                    isUpdate = true;
            }
            if (_job == null)
                _job = new Job() {  CurrentlyEmployed = true, StartDate = DateTime.Now };

        }

        private async Task HandleSubmit()
        {
            _errors = new List<string>();
            if (_job != null)
            {
                if (_job.CurrentlyEmployed == true)
                    _job.EndDate = null;

                if (isUpdate == true)
                {
                    try
                    {
                        await JobService.UpdateAsync(_job);
                    }
                    catch (Exception ex)
                    {
                        _errors = ErrorHelper.GetErrors(ex);
                    }
                }                    
                else                    
                {
                    try
                    {
                        await JobService.CreateAsync(_job);
                    }
                    catch (Exception ex)
                    {
                        _errors = ErrorHelper.GetErrors(ex);
                    }
                }
            }
            NavigationManager.NavigateTo("jobs/list");
        }
    }
}
