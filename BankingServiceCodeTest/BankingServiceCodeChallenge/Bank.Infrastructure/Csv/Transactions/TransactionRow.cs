using BankingServiceCodeTest.Bank.Csv.Transaction;

namespace BankingServiceCodeTest.Bank.Infrastructure.Csv.Transactions;

/// <summary>
/// Model to translate Transactions CSV records into the domain model
/// </summary>
public class TransactionRow
{
    public int TransactionRowId { get; init; }
    public Transaction Transaction { get; init; }
}