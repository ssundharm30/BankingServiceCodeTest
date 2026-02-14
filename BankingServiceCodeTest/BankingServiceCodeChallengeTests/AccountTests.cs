using BankingServiceCodeTest.Bank.Core.Acccount;
using Xunit;

namespace BankingServiceCodeChallengeTests;

public class AccountTests
{
    [Fact]
    public void New_Account_Should_Set_Number_And_Balance()
    {
        var account = new Account("1111234522226789", 1000m);

        Assert.Equal("1111234522226789", account.AccountNumber);
        Assert.Equal(1000m, account.Balance);
    }
    
    [Fact]
    public void New_Account_Should_Throw_When_Balance_Is_Negative()
    {
        Assert.Throws<ArgumentException>(() =>
            new Account("1111234522226789", -1m));
    }
    
    [Fact]
    public void NewAccount_Should_Throw_When_AccountNumber_Is_Invalid()
    {
        Assert.Throws<ArgumentException>(() =>
            new Account("123", 100m));
    }
    
    
    [Fact]
    public void Debit_Should_Reduce_Balance()
    {
        var account = new Account("1111234522226789", 500m);

        account.Debit(100m);

        Assert.Equal(400m, account.Balance);
    }
    
    [Fact]
    public void Debit_Should_Throw_When_Insufficient_Funds()
    {
        var account = new Account("1111234522226789", 100m);

        Assert.Throws<InvalidOperationException>(() =>
            account.Debit(200m));

        Assert.Equal(100m, account.Balance);
    }
    
    [Fact]
    public void Credit_Should_Increase_Balance()
    {
        var account = new Account("1111234522226789", 200m);

        account.Credit(50m);

        Assert.Equal(250m, account.Balance);
    }
    
    [Fact]
    public void Amount_Should_Not_Allow_More_Than_2_Decimal_Places()
    {
        var account = new Account("1111234522226789", 100m);

        Assert.Throws<ArgumentException>(() =>
            account.Debit(10.123m));
    }

}