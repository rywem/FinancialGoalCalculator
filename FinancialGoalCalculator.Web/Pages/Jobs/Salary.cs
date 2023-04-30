using FinancialGoalCalculator.Web.Entities;

namespace FinancialGoalCalculator.Web.Pages.Jobs
{
    public partial class Salary
    {
        [Parameter] public int JobId { get; set; }
        List<JobSalary> _jobSalaries = new List<JobSalary>();
        [Inject] private JobSalaryService JobSalaryService { get; set; }
        private JobSalary _editJobSalary { get; set; } = new JobSalary();
        private JobSalary _newJobSalary { get; set; } = new JobSalary();
        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();   
            //_jobSalaries.Add(new JobSalary {  Id = 1, JobId = 1, SalaryPerYear = 123000, PaychecksPerYear = 26, EffectiveDate = DateTime.Now });
        }
        private async Task LoadAsync()
        {
            _jobSalaries = await JobSalaryService.GetSalariesByJobIdAsync(JobId);
        }

        private async Task HandleSubmitRow()
        {
            await JobSalaryService.UpdateAsync(_editJobSalary);            
            await LoadAsync();
            _editJobSalary.Edit = false;
            _editJobSalary = null;
            _editJobSalary = new JobSalary();
        }
        private async Task HandleNewSalarySubmit()
        {
            _newJobSalary.JobId = JobId;
            await JobSalaryService.CreateAsync(_newJobSalary);
            await LoadAsync();
            _newJobSalary = new JobSalary();
        }

        private void EditRow(JobSalary jobSalary)
        {
            //Ensure all other rows are not being edited
            var otherEdits = _jobSalaries.Where(x => x.Edit == true && x.Id != jobSalary.Id).ToList();
            foreach (var item in otherEdits)
            {
                item.Edit = false;
            }
            if(_editJobSalary != null && _editJobSalary.Id == jobSalary.Id)            
                _editJobSalary = new JobSalary();            
            else
                _editJobSalary = jobSalary;

            jobSalary.Edit = !jobSalary.Edit;
        }
    }
}
