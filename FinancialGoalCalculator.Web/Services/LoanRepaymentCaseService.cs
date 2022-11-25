using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities.Cases;
using FinancialGoalCalculator.Web.Helpers;
using FinancialGoalCalculator.Web.Models;
using FinancialGoalCalculator.Web.Models.Scenarios;
using FinancialGoalCalculator.Web.Pages.Accounts.Loans;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class LoanRepaymentCaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly BalanceService _balanceService;

        public LoanRepaymentCaseService(ApplicationDbContext context, BalanceService balanceService)
        {
            _context = context;
            _balanceService = balanceService;
        }

        public async Task<LoanRepaymentCase> GetFirstOrNewAsync(int scenarioId, int accountId)
        {
            var loanRepaymentCase = await _context.LoanRepaymentCase.FirstOrDefaultAsync(x => x.AccountId == accountId && x.ScenarioId == scenarioId);

            if (loanRepaymentCase != null)
                return loanRepaymentCase;

            return new LoanRepaymentCase()
            {
                ScenarioId = scenarioId,
                AccountId = accountId
            };
        }

        public async Task<LoanRepaymentCase> CreateAsync(LoanRepaymentCase loanRepaymentCase)
        {
            _context.LoanRepaymentCase.Add(loanRepaymentCase);
            await _context.SaveChangesAsync();
            return loanRepaymentCase;
        }

        public async Task<LoanRepaymentCase> UpdateAsync(LoanRepaymentCase loanRepaymentCase)
        {
            var objFromDb = await _context.LoanRepaymentCase.FirstOrDefaultAsync(x => x.Id == loanRepaymentCase.Id);

            if (objFromDb != null)
            {
                objFromDb.PaymentInterval = loanRepaymentCase.PaymentInterval;
                objFromDb.ExtraPayment = loanRepaymentCase.ExtraPayment;
                _context.LoanRepaymentCase.Update(objFromDb);
                await _context.SaveChangesAsync();
            }
            return objFromDb;
        }
        public IEnumerable<LineItemModel> GetLineItems(LoanRepaymentCase loanRepaymentCase)
        {            
            var amortizationList = FinanceHelper.GetAmortizationSchedule(loanRepaymentCase.Account.LoanDetail).ToList();
            
            int elapsedPeriods = ((DateTime.Now.Year - loanRepaymentCase.Account.LoanDetail.FirstPaymentDate.Year) * 12) + DateTime.Now.Month - loanRepaymentCase.Account.LoanDetail.FirstPaymentDate.Month;
            double lenderRate = (double)loanRepaymentCase.Account.LoanDetail.InterestRate;
            int loanPeriodInMonthsRemaining = loanRepaymentCase.Account.LoanDetail.Periods - elapsedPeriods;
            double desiredLoanAmount = (double)loanRepaymentCase.Account.LoanDetail.OriginalBalance;
            var monthlyPayment = Math.Round(FinanceHelper.MonthlyPayment(lenderRate, loanRepaymentCase.Account.LoanDetail.Periods, desiredLoanAmount), 2);

            decimal paymentExtraMonthlyDecimal = 0.0m;

            if(loanRepaymentCase.PaymentInterval == Enum.PaymentInterval.Yearly)
            {
                paymentExtraMonthlyDecimal = Math.Round(loanRepaymentCase.ExtraPayment / 12, 2);
            }
            else if (loanRepaymentCase.PaymentInterval == Enum.PaymentInterval.Quarterly)
            {
                paymentExtraMonthlyDecimal = Math.Round(loanRepaymentCase.ExtraPayment / 3, 2);
            }
            else if (loanRepaymentCase.PaymentInterval == Enum.PaymentInterval.Monthly)
            {
                paymentExtraMonthlyDecimal = Math.Round(loanRepaymentCase.ExtraPayment, 2);
            }
            else if (loanRepaymentCase.PaymentInterval == Enum.PaymentInterval.BiMonthly)
            {
                paymentExtraMonthlyDecimal = Math.Round(loanRepaymentCase.ExtraPayment * 2, 2);
            }
            else if (loanRepaymentCase.PaymentInterval == Enum.PaymentInterval.BiWeekly)
            {
                paymentExtraMonthlyDecimal = Math.Round((loanRepaymentCase.ExtraPayment * 26.0892m) / 12, 2);
            }
            else if (loanRepaymentCase.PaymentInterval == Enum.PaymentInterval.Weekly)
            {
                paymentExtraMonthlyDecimal = Math.Round((loanRepaymentCase.ExtraPayment * 52.1785m) / 12, 2);
            }
            double paymentExtraMonthly = (double)paymentExtraMonthlyDecimal;
            var balanceTask = _balanceService.GetLastBalanceByAccount(loanRepaymentCase.Account);
            balanceTask.Wait();
            var balance = balanceTask.Result;
            DateTime nextMonth = DateTime.Now.AddMonths(1);
            DateTime dateTracking = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            double currentBalance = (double)balance.BalanceAmount;
            var totalInterest = 0.00;
            for (int i = 0; i < loanPeriodInMonthsRemaining; i++)
            {
                dateTracking = dateTracking.AddMonths(1);
                var monthlyInterest = Math.Round(FinanceHelper.InterestTotalForMonth(currentBalance, lenderRate), 2);
                var monthlyPrinciple = (Math.Round((monthlyPayment - monthlyInterest), 2) + paymentExtraMonthly);

                currentBalance -= monthlyPrinciple;
                totalInterest += monthlyInterest;
                if(currentBalance < 0)
                    currentBalance = 0;

                yield return new LineItemModel()
                {
                    AmortizationScheduleModel = new AmortizationScheduleModel
                    {
                        DateOfPayment = dateTracking,
                        InterestAmount = (decimal)monthlyInterest,
                        PeriodNumber = i,
                        PrincipalAmount = (decimal)monthlyPrinciple,
                        PrincipalRemaining = (decimal)currentBalance,
                        ExtraPaymentMonthly = (decimal)paymentExtraMonthlyDecimal,
                    },
                    Date = dateTracking,
                    AccountType = loanRepaymentCase.Account.AccountType,
                    Name = loanRepaymentCase.Account.Name,
                };
                if (currentBalance == 0)
                    break;
            }
        }
    }
}
