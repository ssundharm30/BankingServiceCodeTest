namespace BankingServiceCodeTest.Bank.Application.Transaction;

/// <summary>
/// Model to simply define a transaction failure
/// </summary>
public class TransactionFailure
{
    public int RowNumber { get; set; }
    public string Reason { get; set; }
}