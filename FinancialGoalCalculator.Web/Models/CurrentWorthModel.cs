namespace FinancialGoalCalculator.Web.Models
{
    public class CurrentWorthModel
    {
        public decimal TotalDebt { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalRetirement { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalAssetsRetirement 
        { 
            get 
            {
                return TotalRetirement + TotalAssets;
            } 
        }

        public decimal TotalWorth
        {
            get
            {
                return TotalAssetsRetirement + TotalCash;
            }
        }
        public decimal Networth
        {
            get
            {
                return TotalWorth - TotalDebt;
            }
        }
        public CurrentWorthModel(decimal totalDebt, decimal totalAssets, decimal totalRetirement, decimal totalCash)
        {
            this.TotalDebt = totalDebt;
            this.TotalCash = totalCash;
            this.TotalRetirement = totalRetirement;
            this.TotalAssets = totalAssets;
        }
    }
}
