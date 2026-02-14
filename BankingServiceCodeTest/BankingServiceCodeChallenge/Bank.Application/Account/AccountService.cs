using BankingServiceCodeTest.Bank.Infrastructure.Csv.AccountBalance;
using BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;

namespace BankingServiceCodeTest.Bank.Application.Account;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    } 
    
    public int LoadAccounts(IEnumerable<BalanceRow> rows)
    {
        var count = 0;
        foreach (var r in rows)
        {
            var account = new Core.Acccount.Account(r.Account.AccountNumber, r.Account.Balance);
            _accountRepository.Upsert(account);
            count++;
        }
        return count;
    }

}