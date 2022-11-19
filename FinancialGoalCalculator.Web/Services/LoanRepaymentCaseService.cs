using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities.Cases;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class LoanRepaymentCaseService
    {
        private readonly ApplicationDbContext _context;

        public LoanRepaymentCaseService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
