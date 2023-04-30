using FinancialGoalCalculator.Web.Data;
using FinancialGoalCalculator.Web.Entities;
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
        public async Task<List<JobSalary>> GetSalariesByJobIdAsync(int id)
        {
            return await _context.JobSalary.Where(x => x.JobId == id).ToListAsync();            
        }
        public async Task<JobSalary> UpdateAsync(JobSalary jobSalary)
        {
            if (jobSalary == null)
                throw new ArgumentNullException(nameof(jobSalary));

            var objFromDb = await _context.JobSalary.FirstOrDefaultAsync(x => x.Id == jobSalary.Id);

            if (objFromDb == null)
                throw new Exception($"JobSalary does not exist in DB, unable to update JobSalary, Id: {jobSalary.Id}");


            // update properties
            objFromDb.EffectiveDate = jobSalary.EffectiveDate;
            objFromDb.PaychecksPerYear = jobSalary.PaychecksPerYear;
            objFromDb.SalaryPerYear = jobSalary.SalaryPerYear;            
            _context.JobSalary.Update(jobSalary);
            await _context.SaveChangesAsync();
            return jobSalary;
        }

        public async Task RemoveAsync(int jobSalaryId)
        {
            var objFromDb = await _context.JobSalary.FirstOrDefaultAsync(x => x.Id == jobSalaryId);

            if(objFromDb == null)
                throw new Exception($"JobSalary does not exist in DB, unable to delete JobSalary, Id: {jobSalaryId}");

            _context.JobSalary.Remove(objFromDb);
            await _context.SaveChangesAsync();
        }
    }
}
