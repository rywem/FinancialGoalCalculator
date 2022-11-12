using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialGoalCalculator.Web.Entities.Cases
{
    public class GeneralAssetCase
    {
        [Key]
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public int AccountId { get; set; }
        public decimal GrowthRate { get; set; }
        public decimal Payment { get; set; } = 0;
        [ForeignKey("ScenarioId")]
        public Scenario Scenario { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
