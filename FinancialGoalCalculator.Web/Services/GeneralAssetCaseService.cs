using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities.Cases;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class GeneralAssetCaseService
    {
        private readonly ApplicationDbContext _context;

        public GeneralAssetCaseService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
