using FinancialGoalCalculator.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class JobSalaryService
    {
        private readonly ApplicationDbContext _context;

        public JobSalaryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobSalary> CreateAsync(JobSalary jobSalary)
        {
            if(jobSalary == null)
                throw new ArgumentNullException(nameof(jobSalary));

            var jobFromDb = await _context.Job.FirstOrDefaultAsync(x => x.Id == jobSalary.JobId);

            if(jobFromDb == null)            
                throw new Exception($"Job does not exist, unable to create JobSalary, Id: {jobSalary.JobId}");

            _context.JobSalary.Add(jobSalary);
            await _context.SaveChangesAsync();
            return jobSalary;
        }
    }
}
