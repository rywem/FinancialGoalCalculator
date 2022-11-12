using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities.Cases;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class RealEstateAssetCaseService
    {
        private readonly ApplicationDbContext _context;

        public RealEstateAssetCaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RealEstateAssetCase> GetFirstOrNewAsync(int scenarioId, int accountId)
        {
            var realEstateCase = await _context.RealEstateAssetCase.FirstOrDefaultAsync(x => x.AccountId == accountId && x.ScenarioId == scenarioId);

            if (realEstateCase != null)
                return realEstateCase;
            
            return new RealEstateAssetCase()
            {
                ScenarioId = scenarioId,
                AccountId = accountId
            };
            
        }
    }
}
