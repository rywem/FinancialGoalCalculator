using FinancialGoalCalculator.Web.Enum;

namespace FinancialGoalCalculator.Web.Models.Scenarios
{
    public class LineItemRowModel
    {
        public AccountType AccountType { get; }
        public List<LineItemModel> LineItems { get; }
        public bool Expand { get; set; } = false;
        public LineItemRowModel(AccountType accountType, List<LineItemModel> lineItems)
        {
            AccountType = accountType;
            LineItems = lineItems;
        }
    }
}
