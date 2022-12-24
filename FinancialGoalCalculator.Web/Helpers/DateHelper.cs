namespace FinancialGoalCalculator.Web.Helpers
{
    public class DateHelper
    {
        public static string GetDateOrNA(DateTime? date)
        {
            return date?.ToString("MM/dd/yyyy") ?? "N/A";
        }
    }
}
