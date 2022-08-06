using FinancialGoalCalculator.Web.Helpers;
using FinancialGoalCalculator.Web.Models;

namespace FinancialGoalCalculator.Web.Pages.Accounts.Loans
{
    public partial class Details
    {
        [Parameter] public int AccountId { get; set; }
        [Inject] AccountService _accountService { get; set; }
        [Inject] LoanDetailService _loanDetailService { get; set; }
        LoanDetail _loanDetail { get; set; }
        private List<AmortizationScheduleModel> _amortizationSchedule = new List<AmortizationScheduleModel>();
        decimal payment = 0.0m;
        decimal totalInterest = 0.0m;
        Account _Account;
        private List<string> _errors = new List<string>();
        bool isUpdate = false;
        string result = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            _errors = new List<string>();
            var account = await _accountService.GetAccountByIdAsync(AccountId);
            if(account != null)
            {
                _Account = account;
            }    
            var loanDetail = await _loanDetailService.GetLoanDetailByAccountIdAsync((int)AccountId);
                
            if(loanDetail != null)
            {
                isUpdate = true;
                _loanDetail = loanDetail;
                LoanAmortization();
            }
            else
            {
                isUpdate = false;
                _loanDetail = new LoanDetail();
                _loanDetail.AccountId = AccountId;
            }            
        }

        private void LoanAmortization()
        {
            if (_loanDetail != null)
            {
                _amortizationSchedule = FinanceHelper.GetAmortizationSchedule(_loanDetail).ToList();
                payment = FinanceHelper.MonthlyPayment(_loanDetail);
                totalInterest = _amortizationSchedule.Sum(x => x.InterestAmount);
            }
            
        }
    
        private async Task HandleSubmit()
        {
            _errors = new List<string>();
            if(_loanDetail != null)
            {
                if (isUpdate == true)
                {
                    try
                    {
                        await _loanDetailService.UpdateLoanDetailAsync(_loanDetail);
                        result = "Loan Detail updated successfully!";
                        LoanAmortization();
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
                        await _loanDetailService.AddLoanDetailAsync(_loanDetail);
                        result = "Loan Detail added successfully!";
                        LoanAmortization();

                    }
                    catch (Exception ex)
                    {
                        _errors = ErrorHelper.GetErrors(ex);
                    }
                }
            }
        }
    }
}
