namespace FinancialGoalCalculator.Web.Models.Scenarios
{
    public class YearAggregateModel
    {
        public List<MonthAggregateModel> MonthAggregateModels { get; set;}
        public int Year { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalRetirement { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal Networth => TotalAssets + TotalRetirement - TotalDebt;
        public bool Expand { get; set; } = false;
        public YearAggregateModel()
        {
            MonthAggregateModels = new List<MonthAggregateModel>();

        }

        public void Calculate()
        {
            foreach (var item in MonthAggregateModels)
            {
                item.Calculate();                
            }            

            var lastItem = MonthAggregateModels.OrderByDescending(x => x.Month).FirstOrDefault();
            if(lastItem != null)
            {
                TotalAssets = lastItem.TotalAssets;
                TotalRetirement = lastItem.TotalRetirement;
                TotalDebt = lastItem.TotalDebt;
            }
        }
    }
}
