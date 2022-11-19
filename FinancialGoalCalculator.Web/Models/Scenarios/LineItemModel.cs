using FinancialGoalCalculator.Web.Enum;

namespace FinancialGoalCalculator.Web.Models.Scenarios
{
    public class LineItemModel
    {
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
        public TimeValuePeriodModel TimeValuePeriod { get; set; }
    }
}
