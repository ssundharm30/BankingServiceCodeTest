namespace BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;

public interface IAccountRepository
{
    /// <summary>
    /// Gets an account associated with the account number
    /// or null if there is none
    /// </summary>
    /// <param name="accountNumber"></param>
    /// <returns>Account</returns>
    Core.Acccount.Account? GetAccountByAccountNumber(string accountNumber);
    
    /// <summary>
    /// Creates/Updates an account using a given Account Object
    /// </summary>
    /// <param name="account"></param>
    void Upsert(Core.Acccount.Account account);
    
    /// <summary>
    /// Gets all the accounts present in the in-memory collection
    /// </summary>
    /// <returns></returns>
    IReadOnlyCollection<Core.Acccount.Account> GetAllAccounts();
}