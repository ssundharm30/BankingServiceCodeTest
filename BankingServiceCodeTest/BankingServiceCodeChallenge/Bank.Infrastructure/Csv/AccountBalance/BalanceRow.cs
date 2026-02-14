using BankingServiceCodeTest.Bank.Core.Acccount;

namespace BankingServiceCodeTest.Bank.Infrastructure.Csv.AccountBalance;

/// <summary>
/// Model to translate Balances CSV records into the domain model
/// </summary>
public class BalanceRow
{
    public int BalanceRowId { get; init; }
    public Account Account { get; init; }
}