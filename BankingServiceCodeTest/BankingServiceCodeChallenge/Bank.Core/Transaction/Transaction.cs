namespace BankingServiceCodeTest.Bank.Csv.Transaction;

/// <summary>
/// An immutable transaction record used to identify transactions
/// </summary>
/// <param name="FromAccountNumber"></param>
/// <param name="ToAccountNumber"></param>
/// <param name="Amount"></param>
public record Transaction(string FromAccountNumber, string ToAccountNumber, decimal Amount);