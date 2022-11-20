namespace FinancialGoalCalculator.Web.Models.Scenarios
{
    public class YearAggregateModel
    {
        public List<MonthAggregateModel> MonthAggregateModels { get; set;}
        public int Year { get; set; }

        public YearAggregateModel()
        {
            MonthAggregateModels = new List<MonthAggregateModel>();
        }
    }
}
