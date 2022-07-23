using FinancialGoalCalculator.Web.Entities.Accounts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialGoalCalculator.Web.Entities
{
    public class Balance
    {
        [Key]
        public int Id { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
