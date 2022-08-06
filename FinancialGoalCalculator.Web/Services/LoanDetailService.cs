using FinancialGoalCalculator.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class LoanDetailService
    {
        private readonly ApplicationDbContext _context;

        public LoanDetailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLoanDetailAsync(LoanDetail LoanDetail)
        {
            if (LoanDetail != null)
            {
                _context.LoanDetail.Add(LoanDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateLoanDetailAsync(LoanDetail LoanDetail)
        {
            if (LoanDetail != null && LoanDetail.Id > 0)
            {
                _context.LoanDetail.Update(LoanDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<LoanDetail> GetLoanDetailByIdAsync(int id)
        {
            return await _context.LoanDetail.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<LoanDetail> GetLoanDetailByAccountIdAsync(int accountId)
        {
            return await _context.LoanDetail.FirstOrDefaultAsync(x => x.AccountId == accountId);
        }
    }
}
