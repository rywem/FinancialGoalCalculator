namespace FinancialGoalCalculator.Web.Entities.Interfaces
{
    public interface IAccount
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Created { get; set; }        
    }
}
