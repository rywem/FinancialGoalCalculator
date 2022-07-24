namespace FinancialGoalCalculator.Web.Entities.Interfaces
{
    public interface IAccount
    {
        int Id { get; set; }
        string Name { get; set; }        
        DateTime CreatedDate { get; set; }
        DateTime OpenedDate { get; set; }
        string Owner { get; set; }
    }
}
