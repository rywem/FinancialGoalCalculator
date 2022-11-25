namespace FinancialGoalCalculator.Web.Models
{
    public class AmortizationScheduleModel
    {
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public int PeriodNumber { get; set; }
        public decimal PrincipalRemaining { get; set; }
        public decimal ExtraPaymentMonthly { get; set; }
        public DateTime DateOfPayment { get; set; }
    }
}
