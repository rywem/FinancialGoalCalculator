using FinancialGoalCalculator.Web.Entities.Interfaces;
using FinancialGoalCalculator.Web.Enum;
using System.ComponentModel.DataAnnotations;

namespace FinancialGoalCalculator.Web.Entities.Accounts
{
    public class Account : IAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OpenedDate { get; set; }
        public AccountType AccountType { get; set; }
        public bool Closed { get; set; } = false;
        public virtual ICollection<Balance> Balances { get; set; }
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        public string Owner { get; set; }
        public Account()
        {
            OpenedDate = DateTime.Now;
        }

        public decimal GetLastBalance()
        {
            if (Balances != null && Balances.Count > 0)
                return Balances.OrderByDescending(x => x.Date).FirstOrDefault().BalanceAmount;
            else 
                return 0.0m;
        }
    }
}
