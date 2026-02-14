namespace BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;

public class AccountRepository : IAccountRepository
{
    private readonly Dictionary<string, Core.Acccount.Account> _accounts = new();
    
    public Core.Acccount.Account? GetAccountByAccountNumber(string accountNumber)
    {
        return _accounts.TryGetValue(accountNumber, out var account) ? account : null;
    }

    public void Upsert(Core.Acccount.Account account)
    {
        _accounts[account.AccountNumber] = account;
    }

    public IReadOnlyCollection<Core.Acccount.Account> GetAllAccounts()
    {
        return _accounts.Values.ToList().AsReadOnly();
    }
}