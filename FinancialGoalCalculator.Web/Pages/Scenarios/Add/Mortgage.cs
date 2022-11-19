using FinancialGoalCalculator.Web.Entities.Cases;
using FinancialGoalCalculator.Web.Enum;

namespace FinancialGoalCalculator.Web.Pages.Scenarios.Add
{
    public partial class Mortgage
    {
        [Parameter] public int ScenarioId { get; set; }
        [Parameter] public int AccountId { get; set; }
        [Inject] private LoanRepaymentCaseService LoanRepaymentCaseService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private LoanRepaymentCase _loanRepaymentCase;

        protected override async Task OnInitializedAsync()
        {
            _loanRepaymentCase = await LoanRepaymentCaseService.GetFirstOrNewAsync(ScenarioId, AccountId);
        }

        private async Task HandleSubmit()
        {            
            if (_loanRepaymentCase.Id == 0)
                await LoanRepaymentCaseService.CreateAsync(_loanRepaymentCase);
            else
                await LoanRepaymentCaseService.UpdateAsync(_loanRepaymentCase);

            NavigationManager.NavigateTo($"scenarios/build/{_loanRepaymentCase.ScenarioId}");
        }

        private void OnPaymentIntervalValueChanged(PaymentInterval value)
        {
            _loanRepaymentCase.PaymentInterval = value;
        }
    }
}
