using FinancialGoalCalculator.Web.Entities;
using FinancialGoalCalculator.Web.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialGoalCalculator.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Balance> Balance { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobSalary> JobSalary { get; set; }
        public DbSet<Scenario> Scenario { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
