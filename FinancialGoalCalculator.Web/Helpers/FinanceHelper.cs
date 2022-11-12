using FinancialGoalCalculator.Web.Entities.Cases;
using FinancialGoalCalculator.Web.Models;

namespace FinancialGoalCalculator.Web.Helpers
{
    public static class FinanceHelper
    {

        public static IEnumerable<FutureValueScheduleModel> GetRealEstateAssetSchedule(Account account, GeneralAssetCase generalAssetCase, int years)
        {
            int periods = years * 12;
            decimal monthlyInterestRate = generalAssetCase.GrowthRate / 12 / 100;
            decimal futureValue = account.GetLastBalance();            
            DateTime currentDate = account.GetLastBalanceDate();
            for (int i = 0; i < periods; i++)
            {
                var schedule = GetScheduleForPeriod(futureValue, generalAssetCase.Payment, monthlyInterestRate, i, currentDate);
                futureValue = schedule.EstimatedValue;
                yield return schedule;
            }            
        }
        
        private static FutureValueScheduleModel GetScheduleForPeriod(decimal futureValue, decimal monthlyInvestment, decimal monthlyInterestRate, int period, DateTime startDate)
        {
            var schedule = new FutureValueScheduleModel();
            schedule.PeriodGrowthRate = monthlyInterestRate;
            schedule.PeriodNumber = period;
            schedule.EstimatedValue = (futureValue + monthlyInvestment) * (1 + monthlyInterestRate);
            schedule.Payment = monthlyInvestment;
            var tempDate = startDate.AddMonths(1);
            schedule.EstimateDate = new DateTime(tempDate.Year, tempDate.Month, 1);
            return schedule;
        }


        public static IEnumerable<AmortizationScheduleModel> GetAmortizationSchedule(LoanDetail detail)
        {
            double lenderRate = (double)detail.InterestRate;
            int loanPeriodInMonths = detail.Periods;
            double desiredLoanAmount = (double)detail.OriginalBalance;
            var monthlyPayment = Math.Round(MonthlyPayment(lenderRate, loanPeriodInMonths, desiredLoanAmount), 2);
            Console.WriteLine($"Repayment Amount: {monthlyPayment}");
            var balance = desiredLoanAmount;
            var totalInterest = 0.00;

            for (var i = 0; i < loanPeriodInMonths; i++)
            {
                DateTime dateOfPayment = detail.FirstPaymentDate.AddMonths(i);
                //var monthlyInterest = Math.Round(balance * ((lenderRate / 100 / 12)), 3);
                var monthlyInterest = Math.Round(InterestTotalForMonth(balance, lenderRate), 2);
                var monthlyPrinciple = Math.Round((monthlyPayment - monthlyInterest), 2);
                
                balance -= Math.Round(monthlyPayment - monthlyInterest, 2);
                totalInterest += monthlyInterest;

                yield return new AmortizationScheduleModel
                {
                    DateOfPayment = dateOfPayment,
                    InterestAmount = (decimal)monthlyInterest,
                    PeriodNumber = i,
                    PrincipalAmount = (decimal)monthlyPrinciple,
                    PrincipalRemaining = (decimal)balance
                };
            }
             
        }

        public static void CompoundCalculator(double lenderRate, int loanPeriodInMonths, double desiredLoanAmount, DateTime startDate)
        {
            // source: https://stackoverflow.com/questions/46494528/amortization-schedule-calculation-with-differing-results
            var monthlyPayment = Math.Round(MonthlyPayment(lenderRate, loanPeriodInMonths, desiredLoanAmount), 2);
            Console.WriteLine($"Repayment Amount: {monthlyPayment}");
            var balance = desiredLoanAmount;
            var totalInterest = 0.00;

            for (var i = 0; i < loanPeriodInMonths; i++)
            {
                DateTime dateOfPayment = startDate.AddMonths(i);
                //var monthlyInterest = Math.Round(balance * ((lenderRate / 100/ 12)), 2);
                var monthlyInterest = Math.Round(InterestTotalForMonth(balance, lenderRate), 2);
                var monthlyPrinciple = Math.Round((monthlyPayment - monthlyInterest), 2);
                
                balance -= Math.Round(monthlyPayment - monthlyInterest, 2);
                totalInterest += monthlyInterest;

                Console.WriteLine($"Period: {i}, Interest: {monthlyInterest}, Principle: {monthlyPrinciple}, Date: {dateOfPayment.ToString("MM/dd/yyyy")}");
            }
            Console.WriteLine($"Balance: {balance}, Total Interest {totalInterest}");          
        }

        public static double InterestTotalForMonth(double balance, double lenderRate)
        {
            if (balance <= 0)
                return 0;
            if (lenderRate == 0)
                return 0;

            return balance * (lenderRate / 100 / 12);
        }

        public static decimal MonthlyPayment(LoanDetail detail)
        {
            return (decimal)MonthlyPayment((double)detail.InterestRate, detail.Periods, (double)detail.OriginalBalance);
        }
        public static double MonthlyPayment(double interestRate, int totalNumberOfPeriods, double originalBalance)
        {
            //https://www.comeausoftware.com/2019/03/calculating-mortgage-payments-csharp/

            double returnPayment = 0;
            double monthlyInterest = ((double)interestRate * 0.01) / 12;
            double loanAmt = (double)originalBalance;
            
            try
            {
                if (loanAmt > 0 && interestRate > 0)
                    returnPayment = loanAmt * ((monthlyInterest * Math.Pow((1 + monthlyInterest),
                      totalNumberOfPeriods)) / (Math.Pow(1 + monthlyInterest, totalNumberOfPeriods) - 1));
                else
                    returnPayment = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnPayment;
        }
    }
}
