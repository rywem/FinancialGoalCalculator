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


            return null;
        }
    }
}
