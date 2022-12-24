using System.ComponentModel.DataAnnotations;

namespace FinancialGoalCalculator.Web.Entities
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }    
        public DateTime? EndDate { get; set; }
        public bool CurrentlyEmployed { get; set; }
        public ICollection<JobSalary> JobSalaries { get; set; }
    }
}
