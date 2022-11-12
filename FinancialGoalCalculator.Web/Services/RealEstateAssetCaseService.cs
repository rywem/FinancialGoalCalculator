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

        public async Task<RealEstateAssetCase> CreateAsync(RealEstateAssetCase realEstateAssetCase)
        {
            _context.RealEstateAssetCase.Add(realEstateAssetCase);
            await _context.SaveChangesAsync();
            return realEstateAssetCase;
        }

        public async Task<RealEstateAssetCase> UpdateAsync(RealEstateAssetCase realEstateAssetCase)
        {
            var objFromDb = await _context.RealEstateAssetCase.FirstOrDefaultAsync(x => x.Id == realEstateAssetCase.Id);

            if(objFromDb != null)
            {
                objFromDb.GrowthRate = realEstateAssetCase.GrowthRate;
                _context.RealEstateAssetCase.Update(objFromDb);
                await _context.SaveChangesAsync();
            }
            return objFromDb;
        }
    }
}
