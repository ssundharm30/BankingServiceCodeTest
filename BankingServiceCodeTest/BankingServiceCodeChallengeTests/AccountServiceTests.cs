using BankingServiceCodeTest.Bank.Application.Account;
using BankingServiceCodeTest.Bank.Core.Acccount;
using BankingServiceCodeTest.Bank.Infrastructure.Csv.AccountBalance;
using BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;
using Xunit;

namespace BankingServiceCodeChallengeTests;

public class AccountServiceTests
{
    [Fact]
    public void Load_creates_accounts_for_each_balance_row_and_returns_count()
    {
        // Arrange
        var repo = new AccountRepository();
        var accountService = new AccountService(repo);

        var rows = new[]
        {   
            new BalanceRow
            {
                BalanceRowId = 2,
                Account = new Account("1111234522226789", 5000m)
            },
            new BalanceRow
            {
                BalanceRowId = 3,
                Account = new Account("1111234522221234", 10000m)
            },
            new BalanceRow
            {
                BalanceRowId = 4,
                Account = new Account("2222123433331212", 550m)
            }
        };

        // Act
        var loadedAccounts = accountService.LoadAccounts(rows);

        // Assert
        Assert.Equal(3, loadedAccounts);
        Assert.Equal(3, repo.GetAllAccounts().Count);
        Assert.Equal(5000.00m, repo.GetAccountByAccountNumber("1111234522226789")!.Balance);
        Assert.Equal(10000.00m, repo.GetAccountByAccountNumber("1111234522221234")!.Balance);
        Assert.Equal(550.00m, repo.GetAccountByAccountNumber("2222123433331212")!.Balance);
    }
    
    
    [Fact]
    public void Load_overwrites_existing_account_balance_when_same_account_number_appears()
    {
        // Arrange
        var repo = new AccountRepository();
        repo.Upsert(new Account("1111234522226789", 1.00m));

        var accountService = new AccountService(repo);

        var rows = new[]
        {
            new BalanceRow
            {
                BalanceRowId = 2,
                Account = new Account("1111234522226789", 5000m)
            }
        };

        // Act
        var loaded = accountService.LoadAccounts(rows);

        // Assert
        Assert.Equal(1, loaded);
        Assert.Single(repo.GetAllAccounts());
        Assert.Equal(5000.00m, repo.GetAccountByAccountNumber("1111234522226789")!.Balance);
    }
}