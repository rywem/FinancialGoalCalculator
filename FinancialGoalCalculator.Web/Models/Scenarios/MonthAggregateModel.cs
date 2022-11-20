namespace FinancialGoalCalculator.Web.Models.Scenarios
{
    public class MonthAggregateModel
    {
        public Dictionary<string, LineItemModel> LineItems { get; set; }
        public int Month { get; set; }
    }
}
