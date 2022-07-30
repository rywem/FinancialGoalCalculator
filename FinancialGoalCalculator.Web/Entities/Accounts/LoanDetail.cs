using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialGoalCalculator.Web.Entities.Accounts
{
    public class LoanDetail
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal OriginalBalance { get; set; }
        public DateTime FirstPaymentDate { get; set; }
        public decimal InterestRate { get; set; }
        public int Periods { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
