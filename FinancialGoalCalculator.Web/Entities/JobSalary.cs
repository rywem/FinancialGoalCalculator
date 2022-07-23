using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialGoalCalculator.Web.Entities
{
    public class JobSalary
    {
        [Key]
        public int Id { get; set; }
        public decimal SalaryPerYear { get; set; }
        public int PaychecksPerYear { get; set; }
        public DateTime EffectiveDate { get; set; }        
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
    }
}
