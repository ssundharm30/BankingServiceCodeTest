using BankingServiceCodeTest.Bank.Infrastructure.Csv.AccountBalance;

namespace BankingServiceCodeTest.Bank.Application.Account;

public interface IAccountService
{
    /// <summary>
    /// Load accounts from balance records parsed from 
    /// the balance csv file into an in-memory collection
    /// </summary>
    /// <param name="rows"></param>
    /// <returns>Number of accounts loaded</returns>
    int LoadAccounts(IEnumerable<BalanceRow> rows);
}