using BankingServiceCodeTest.Bank.Application.Transaction;
using BankingServiceCodeTest.Bank.Core.Acccount;
using BankingServiceCodeTest.Bank.Csv.Transaction;
using BankingServiceCodeTest.Bank.Infrastructure.Csv.Transactions;
using BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;
using Xunit;

namespace BankingServiceCodeChallengeTests;

public class TransactionServiceTests
{
    [Fact]
    public void Should_Process_Valid_Transaction_Successfully()
    {
        var repo = new AccountRepository();

        repo.Upsert(new Account("1111234522226789", 500m));
        repo.Upsert(new Account("1212343433335665", 100m));

        var transactionService = new TransactionService(repo);

        var rows = new[]
        {
            new TransactionRow()
            {
                TransactionRowId = 2,
                Transaction = new Transaction("1111234522226789", "1212343433335665", 200m)
            }
        };

        var result = transactionService.ProcessTransaction(rows);

        Assert.Equal(1, result.Succeeded);
        Assert.Equal(300m, repo.GetAccountByAccountNumber("1111234522226789")!.Balance);
        Assert.Equal(300m, repo.GetAccountByAccountNumber("1212343433335665")!.Balance);
    }
    
    [Fact]
    public void Should_Fail_When_Insufficient_Funds()
    {
        var repo = new AccountRepository();

        repo.Upsert(new Account("1111234522226789", 100m));
        repo.Upsert(new Account("1212343433335665", 0m));

        var transactionService = new TransactionService(repo);

        var rows = new[]
        {
            new TransactionRow()
            {
                TransactionRowId = 2,
                Transaction = new Transaction("1111234522226789", "1212343433335665", 200m)
            }
        };

        var result = transactionService.ProcessTransaction(rows);

        Assert.Equal(0, result.Succeeded);
        Assert.Single(result.Failed);
        Assert.Equal(100m, repo.GetAccountByAccountNumber("1111234522226789")!.Balance);
    }
    
    [Fact]
    public void Should_Fail_When_From_Account_Not_Found()
    {
        var repo = new AccountRepository();
        repo.Upsert(new Account("1212343433335665", 100m));

        var transactionService = new TransactionService(repo);

        var rows = new[]
        {
            new TransactionRow()
            {
                TransactionRowId = 2,
                Transaction = new Transaction("9999999999999999", "1212343433335665", 50m)
            }
        };

        var result = transactionService.ProcessTransaction(rows);

        Assert.Equal(0, result.Succeeded);
        Assert.Single(result.Failed);
        Assert.Equal("Source account not found",
            result.Failed[0].Reason);
    }
    
    [Fact]
    public void Processes_transactions_in_order_and_rejects_insufficient_funds()
    {
        var repo = new AccountRepository();
        repo.Upsert(new Account("1111234522226789" ,500m));
        repo.Upsert(new Account("1212343433335665", 0m));

        var transactionService = new TransactionService(repo);

        var rows = new[]
        {
            new TransactionRow()
            {
                TransactionRowId = 2,
                Transaction = new Transaction("1111234522226789", "1212343433335665", 200m)
            },
            new TransactionRow()
            {
                TransactionRowId = 3,
                Transaction = new Transaction("1111234522226789", "1212343433335665", 400m)
            }
        };

        var result = transactionService.ProcessTransaction(rows);

        Assert.Equal(2, result.Processed);
        Assert.Equal(1, result.Succeeded);
        Assert.Single(result.Failed);
        Assert.Equal("Transaction failed due to Insufficient funds", result.Failed[0].Reason);

        Assert.Equal(300m, repo.GetAccountByAccountNumber("1111234522226789")!.Balance);
        Assert.Equal(200m, repo.GetAccountByAccountNumber("1212343433335665")!.Balance);
    }
}