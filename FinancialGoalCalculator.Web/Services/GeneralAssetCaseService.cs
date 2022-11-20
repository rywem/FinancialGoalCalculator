using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities.Cases;
using FinancialGoalCalculator.Web.Helpers;
using FinancialGoalCalculator.Web.Models.Scenarios;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class GeneralAssetCaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly BalanceService _balanceService;

        public GeneralAssetCaseService(ApplicationDbContext context, BalanceService balanceService)
        {
            _context = context;
            _balanceService = balanceService;
        }

        public async Task<GeneralAssetCase> GetFirstOrNewAsync(int scenarioId, int accountId)
        {
            var realEstateCase = await _context.GeneralAssetCase.FirstOrDefaultAsync(x => x.AccountId == accountId && x.ScenarioId == scenarioId);

            if (realEstateCase != null)
                return realEstateCase;
            
            return new GeneralAssetCase()
            {
                ScenarioId = scenarioId,
                AccountId = accountId
            };            
        }

        public async Task<GeneralAssetCase> CreateAsync(GeneralAssetCase realEstateAssetCase)
        {
            _context.GeneralAssetCase.Add(realEstateAssetCase);
            await _context.SaveChangesAsync();
            return realEstateAssetCase;
        }

        public async Task<GeneralAssetCase> UpdateAsync(GeneralAssetCase realEstateAssetCase)
        {
            var objFromDb = await _context.GeneralAssetCase.FirstOrDefaultAsync(x => x.Id == realEstateAssetCase.Id);

            if(objFromDb != null)
            {
                objFromDb.GrowthRate = realEstateAssetCase.GrowthRate;
                _context.GeneralAssetCase.Update(objFromDb);
                await _context.SaveChangesAsync();
            }
            return objFromDb;
        }

        public IEnumerable<LineItemModel> GetLineItems(GeneralAssetCase generalAssetCase, int years)
        {
            int nper = years * (int)generalAssetCase.PaymentInterval;
            decimal interestPerPeriod = generalAssetCase.GrowthRate / (int)generalAssetCase.PaymentInterval / 100;
            //decimal presentValue = generalAsset
            var balanceTask = _balanceService.GetLastBalanceByAccount(generalAssetCase.Account);
            balanceTask.Wait();
            var balance = balanceTask.Result;
            decimal futureValue = balance.BalanceAmount;
            DateTime nextMonth = DateTime.Now.AddMonths(1);            
            DateTime dateTracking = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            for (int i = 0; i < nper; i++)
            {
                LineItemModel lineItem = new LineItemModel();
                lineItem.Name = generalAssetCase.Account.Name;
                lineItem.AccountType = generalAssetCase.Account.AccountType;
                lineItem.TimeValuePeriod.PeriodNumber = i;
                lineItem.TimeValuePeriod.PresentValue = futureValue;
                lineItem.TimeValuePeriod.Payment = generalAssetCase.Payment;
                lineItem.TimeValuePeriod.FutureValue = FinanceHelper.CalculateFutureValueSpecificPeriod(futureValue + generalAssetCase.Payment, interestPerPeriod, 1);
                if(generalAssetCase.PaymentInterval == Enum.PaymentInterval.Monthly)
                {
                    lineItem.TimeValuePeriod.Date = dateTracking.AddMonths(1);
                }
                else if (generalAssetCase.PaymentInterval == Enum.PaymentInterval.Yearly)
                {
                    dateTracking = dateTracking.AddYears(1);
                }
                else if (generalAssetCase.PaymentInterval == Enum.PaymentInterval.BiAnnually)
                {
                    dateTracking = dateTracking.AddMonths(6);
                }
                else if (generalAssetCase.PaymentInterval == Enum.PaymentInterval.Quarterly)
                {
                    dateTracking = dateTracking.AddMonths(3);
                }
                else if (generalAssetCase.PaymentInterval == Enum.PaymentInterval.BiMonthly)
                {
                    if (dateTracking.Day == 1)
                    {
                        dateTracking = dateTracking.AddDays(14);
                    }
                    else if (dateTracking.Day == 15)
                    {
                        dateTracking = dateTracking.AddMonths(1).AddDays(-14);                        
                    }                    
                }
                else if (generalAssetCase.PaymentInterval == Enum.PaymentInterval.BiWeekly)
                {
                    dateTracking = dateTracking.AddDays(14);
                }
                else if (generalAssetCase.PaymentInterval == Enum.PaymentInterval.Weekly)
                {
                    dateTracking = dateTracking.AddDays(7);
                }
                lineItem.TimeValuePeriod.Date = dateTracking;
                lineItem.TimeValuePeriod.PeriodInterestRate = interestPerPeriod;
                futureValue = lineItem.TimeValuePeriod.FutureValue;
                yield return lineItem;
            }
        }
    }
}
