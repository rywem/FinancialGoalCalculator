using FinancialGoalCalculator.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoalCalculator.Web.Services
{
    public class JobService
    {
        private readonly ApplicationDbContext _context;

        public JobService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            return await _context.Job                
                .ToListAsync();
        }

        public async Task<List<Job>> GetActiveJobsAsync()
        {
            return await _context.Job
                .Where(x => x.CurrentlyEmployed == true)                
                .ToListAsync();
        }

        public async Task<Job> GetJobAsync(int id)
        {
            return await _context.Job
                .Include("JobSalaries")
                .FirstOrDefaultAsync(x => x.Id == id);                
        }


        public async Task<Job> CreateAsync(Job job)
        {
            if(job == null)
                throw new ArgumentNullException(nameof(job));

            _context.Job.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job> UpdateAsync(Job job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            var objFromDb = await _context.Job.FirstOrDefaultAsync(x => x.Id == job.Id);

            if (objFromDb == null)
                throw new Exception("Job in DB does not exist");

            objFromDb.StartDate = job.StartDate;
            objFromDb.EndDate = job.EndDate;
            objFromDb.EmployeeName = job.EmployeeName;
            objFromDb.Company = job.Company;
            objFromDb.CurrentlyEmployed = job.CurrentlyEmployed;
            objFromDb.Title = job.Title;

            _context.Job.Update(objFromDb);
            await _context.SaveChangesAsync();
            return objFromDb;
        }
    }
}
