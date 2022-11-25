namespace FinancialGoalCalculator.Web.Models.Scenarios
{
    public class MonthAggregateModel
    {
        public Dictionary<string, LineItemRowModel> LineItemsRow { get; set; }
        public int Month { get; set; }

        public decimal TotalAssets { get; set; }      
        public decimal TotalRetirement { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal Networth => TotalAssets + TotalRetirement - TotalDebt;
        public bool Expand { get; set; } = false;
        public MonthAggregateModel()
        {
            LineItemsRow = new Dictionary<string, LineItemRowModel>();
        }

        public void Calculate()
        {
            TotalAssets = GetTotalAssets();
            TotalDebt = GetTotalDebt();
            TotalRetirement = GetTotalRetirement();
        }
        public decimal GetTotalAssets()
        {
            decimal totalAssets = 0;
            foreach (var item in LineItemsRow)
            {
                var assets = item.Value.LineItems.Where(x => x.TimeValuePeriod != null && (x.AccountType == Enum.AccountType.Asset || x.AccountType == Enum.AccountType.RealEstateAsset)).ToList();
                if (assets.Any())
                {
                    totalAssets += assets.OrderByDescending(x => x.Date).First().TimeValuePeriod.FutureValue;
                }
            }
            return totalAssets;
        }
        public decimal GetTotalRetirement()
        {
            decimal totalAssets = 0;
            foreach (var item in LineItemsRow)
            {
                var assets = item.Value.LineItems.Where(x => x.TimeValuePeriod != null && x.AccountType == Enum.AccountType.RetirementAccount).ToList();
                if (assets.Any())
                {
                    totalAssets += assets.OrderByDescending(x => x.Date).First().TimeValuePeriod.FutureValue;
                }
            }
            return totalAssets;
        }
        public decimal GetTotalDebt()
        {
            decimal totalDebt = 0;
            foreach (var item in LineItemsRow)
            {
                var debts = item.Value.LineItems.Where(x => x.AmortizationScheduleModel != null && (x.AccountType == Enum.AccountType.Debt || x.AccountType == Enum.AccountType.Mortgage)).ToList();
                if (debts.Any())
                {
                    totalDebt += debts.OrderByDescending(x => x.Date).First().AmortizationScheduleModel.PrincipalRemaining;
                }
            }
            return totalDebt;
        }
    }
}
