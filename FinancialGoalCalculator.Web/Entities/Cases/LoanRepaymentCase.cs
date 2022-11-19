using FinancialGoalCalculator.Web.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialGoalCalculator.Web.Entities.Cases
{
    public class LoanRepaymentCase
    {
        [Key]
        public int Id { get; set; }
        public decimal ExtraPayment { get; set; }
        public PaymentInterval PaymentInterval { get; set; }
        public int ScenarioId { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("ScenarioId")]
        public Scenario Scenario { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
