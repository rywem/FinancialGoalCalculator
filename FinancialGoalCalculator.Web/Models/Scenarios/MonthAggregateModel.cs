namespace FinancialGoalCalculator.Web.Models.Scenarios
{
    public class MonthAggregateModel
    {
        public Dictionary<string, List<LineItemModel>> LineItems { get; set; }
        public int Month { get; set; }

        public MonthAggregateModel()
        {
            LineItems = new Dictionary<string, List<LineItemModel>>();
        }
    }
}
