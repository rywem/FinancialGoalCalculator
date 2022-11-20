namespace FinancialGoalCalculator.Web.Models
{
    public class TimeValuePeriodModel
    {
        public decimal PresentValue { get; set; }
        public decimal FutureValue { get; set; }
        public int PeriodNumber { get; set; }
        public decimal PeriodInterestRate { get; set; }
        public decimal Payment { get; set; }
        public DateTime Date { get; set; }
    }
}
