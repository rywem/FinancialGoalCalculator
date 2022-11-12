using FinancialGoalCalculator.Web.Entities.Cases;
using System.ComponentModel.DataAnnotations;

namespace FinancialGoalCalculator.Web.Entities
{
    public class Scenario
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }        
        public ICollection<GeneralAssetCase> RealEstateAssetCases { get; set; }
    }
}
