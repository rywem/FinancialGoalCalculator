using FinancialGoalCalculator.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class ScenarioService
    {
        private readonly ApplicationDbContext _context;

        public ScenarioService(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task GenerateScenario(int scenarioId)
        {

        }
    }
}
