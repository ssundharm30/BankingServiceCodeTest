namespace BankingServiceCodeTest.Bank.Application.Transaction;

/// <summary>
/// Object to return a meaningful transaction process result
/// </summary>
public class TransactionResult
{
    public int Processed { get; set; }
    public int Succeeded { get; set; }
    public List<TransactionFailure> Failed { get; } = new();
}
