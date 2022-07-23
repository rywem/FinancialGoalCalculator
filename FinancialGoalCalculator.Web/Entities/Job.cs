using System.ComponentModel.DataAnnotations;

namespace FinancialGoalCalculator.Web.Entities
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }        
    }
}
