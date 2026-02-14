using BankingServiceCodeTest.Bank.Core.Acccount;
using BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;
using Xunit;

namespace BankingServiceCodeChallengeTests;

public class AccountRepositoryTests
{
    [Fact]
    public void Get_returns_null_when_account_does_not_exist()
    {
        // Arrange
        var repo = new AccountRepository();

        // Act
        var account = repo.GetAccountByAccountNumber("1111234522226789");

        // Assert
        Assert.Null(account);
    }
    
    [Fact]
    public void Upsert_then_Get_returns_same_account_instance()
    {
        // Arrange
        var repo = new AccountRepository();
        var account = new Account("1111234522226789", 5000.00m);

        // Act
        repo.Upsert(account);
        var result = repo.GetAccountByAccountNumber("1111234522226789");

        // Assert
        Assert.NotNull(result);
        Assert.Same(account, result);
        Assert.Equal("1111234522226789", result!.AccountNumber);
        Assert.Equal(5000.00m, result.Balance);
    }
    
    [Fact]
    public void Upsert_overwrites_existing_account_with_same_number()
    {
        // Arrange
        var repo = new AccountRepository();
        var first = new Account("1111234522226789", 100.00m);
        var second = new Account("1111234522226789", 999.00m);

        repo.Upsert(first);

        // Act
        repo.Upsert(second);
        var account = repo.GetAccountByAccountNumber("1111234522226789");

        // Assert
        Assert.NotNull(account);
        Assert.Same(second, account);
        Assert.Equal(999.00m, account!.Balance);
    }
    
    [Fact]
    public void GetAll_returns_all_accounts_currently_stored()
    {
        // Arrange
        var repo = new AccountRepository();
        repo.Upsert(new Account("1111234522226789", 10.00m));
        repo.Upsert(new Account("1111234522221234", 20.00m));
        repo.Upsert(new Account("2222123433331212", 30.00m));

        // Act
        var accounts = repo.GetAllAccounts();

        // Assert
        Assert.Equal(3, accounts.Count);

        var accountNumbers = accounts.Select(account => account.AccountNumber).ToHashSet();
        Assert.Contains("1111234522226789", accountNumbers);
        Assert.Contains("1111234522221234", accountNumbers);
        Assert.Contains("2222123433331212", accountNumbers);
    }
    
    [Fact]
    public void Repository_can_store_multiple_accounts_and_retrieve_by_key_independently()
    {
        // Arrange
        var repo = new AccountRepository();
        var account1 = new Account("1111234522226789", 100.00m);
        var account2 = new Account("3212343433335755", 500.00m);

        repo.Upsert(account1);
        repo.Upsert(account2);

        // Act
        var account1result = repo.GetAccountByAccountNumber("1111234522226789");
        var account2result = repo.GetAccountByAccountNumber("3212343433335755");

        // Assert
        Assert.Same(account1, account1result);
        Assert.Same(account2, account2result);
        Assert.Equal(100.00m, account1result!.Balance);
        Assert.Equal(500.00m, account2result!.Balance);
    }
}