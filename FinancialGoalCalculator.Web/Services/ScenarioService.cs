using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Models.Scenarios;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class ScenarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly GeneralAssetCaseService _generalAssetCaseService;
        private readonly LoanRepaymentCaseService _loanRepaymentCaseService;

        public ScenarioService(ApplicationDbContext context, GeneralAssetCaseService generalAssetCaseService, LoanRepaymentCaseService loanRepaymentCaseService)
        {
            _context = context;
            _generalAssetCaseService = generalAssetCaseService;
            _loanRepaymentCaseService = loanRepaymentCaseService;
        }
        public async Task<List<Scenario>> GetScenariosAsync()
        {
            return await _context.Scenario.ToListAsync();
        }

        public async Task AddScenarioAsync(Scenario scenario)
        {
            if (scenario != null)
            {
                scenario.Created = DateTime.Now;
                _context.Scenario.Add(scenario);
                await _context.SaveChangesAsync();
            }
        }


        //public async Task<List<YearAggregateModel>> GenerateScenario(int scenarioId, int years)
        public async Task<List<YearAggregateModel>> GenerateScenario(int scenarioId, int years)
        {
            var scenario = await _context.Scenario
                .Include("GeneralAssetCases")
                .Include("GeneralAssetCases.Account")
                .Include("LoanRepaymentCases")
                .Include("LoanRepaymentCases.Account")
                .Include("LoanRepaymentCases.Account.LoanDetail")
                .FirstOrDefaultAsync(x => x.Id == scenarioId);

            List<LineItemModel> lineItems = new List<LineItemModel>();
            foreach (var item in scenario.GeneralAssetCases)
            {
                lineItems.AddRange(_generalAssetCaseService.GetLineItems(item, years));
            }

            foreach (var item in scenario.LoanRepaymentCases)
            {
                lineItems.AddRange(_loanRepaymentCaseService.GetLineItems(item));
            }

            List<YearAggregateModel> yearAggregates= new List<YearAggregateModel>();
            DateTime nextMonth = DateTime.Now.AddMonths(1);            
            DateTime dateTracking = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            int nbrMonths = years * 12;
            for (int i = 0; i < nbrMonths; i++)
            {
                dateTracking = dateTracking.AddMonths(i);

                if (!yearAggregates.Any(x => x.Year == dateTracking.Year))
                {
                    YearAggregateModel yearAggregate = new YearAggregateModel();
                    yearAggregate.Year = dateTracking.Year;
                    yearAggregates.Add(yearAggregate);
                }

                var currentYearAggregate = yearAggregates.First(x => x.Year == dateTracking.Year);

                if(!currentYearAggregate.MonthAggregateModels.Any(x => x.Month == dateTracking.Month))
                {
                    MonthAggregateModel monthAggregate = new MonthAggregateModel();
                    monthAggregate.Month = dateTracking.Month;
                    currentYearAggregate.MonthAggregateModels.Add(monthAggregate);
                }

                var currentMonthAggregate = currentYearAggregate.MonthAggregateModels.First(x => x.Month == dateTracking.Month);

                var lineItemsForMonth = lineItems.Where(x => x.Date.Year == dateTracking.Year && x.Date.Month == dateTracking.Month).ToList();

                foreach (var item in lineItemsForMonth)
                {
                    if (!currentMonthAggregate.LineItems.ContainsKey(item.Name))
                    {
                        currentMonthAggregate.LineItems.TryAdd(item.Name, new List<LineItemModel>());
                    }

                    List<LineItemModel> list = null;
                    if (currentMonthAggregate.LineItems.TryGetValue(item.Name, out list))
                    {
                        list.Add(item);
                    }
                }
            }
            return yearAggregates;
            //for (int i = years; i < years; i++)
            //{
            //    YearAggregateModel yearAggregate = new YearAggregateModel();
            //    DateTime dateTracking = startDate.AddYears(i);
            //    yearAggregate.Year = dateTracking.Year;
            //    for (int j = 1; j <= 12; j++)
            //    {
            //        MonthAggregateModel monthAggregate = new MonthAggregateModel();
            //    }
            //}            
        }
    }
}
