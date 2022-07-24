namespace FinancialGoalCalculator.Web.Helpers
{
    public class ErrorHelper
    {
        public static List<string> GetErrors(Exception ex)
        {
            var errors = new List<string>();

            if(ex != null)
            {
                Exception currentEx = ex;

                while(currentEx != null)
                {
                    errors.Add(currentEx.Message);
                    currentEx = currentEx.InnerException;
                }
            }
            return errors;
        }
    }
}
