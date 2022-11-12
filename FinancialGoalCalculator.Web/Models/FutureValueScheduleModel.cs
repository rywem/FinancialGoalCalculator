namespace FinancialGoalCalculator.Web.Models
{
    public class FutureValueScheduleModel
    {
        public decimal EstimatedValue { get; set; }
        public int PeriodNumber { get; set; }
        public decimal PeriodGrowthRate { get; set; }
        public decimal Payment { get; set; }
        public DateTime EstimateDate { get; set; }
    }
}
