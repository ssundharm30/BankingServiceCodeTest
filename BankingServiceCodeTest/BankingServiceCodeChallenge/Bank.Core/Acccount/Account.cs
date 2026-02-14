namespace BankingServiceCodeTest.Bank.Core.Acccount;

/// <summary>
/// Account domain model
/// </summary>
public class Account
{
    public string AccountNumber { get; }
    public decimal Balance { get; private set; }

    public Account(string accountNumber, decimal openingBalance)
    {
        ValidateAccountNumber(accountNumber);
        if (openingBalance < 0m)
        {
            throw new ArgumentException("Opening balance must be greater than 0");
        }
        AccountNumber = accountNumber;
        Balance = Decimal.Round(openingBalance, 2);
    }

    public void Debit(decimal amount)
    {
        ValidateAmount(amount);
        if (!CanDebit(amount))
        {
            throw new InvalidOperationException("Insufficient funds");
        }
        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        ValidateAmount(amount);
        Balance += amount;
    }
    
    public bool CanDebit(decimal amount)
    {
        ValidateAmount(amount);
        
        return Balance - amount > 0m;
    }

    #region private methods

    private static void ValidateAccountNumber(string accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            throw new ArgumentException("Account number is required");
        }

        if (accountNumber.Length != 16)
        {
            throw new  ArgumentException("Account number must be 16 characters long");
        }
    }

    private static void ValidateAmount(decimal amount)
    {
        if (amount < 0m)
        {
            throw new ArgumentException("Balance must be greater than or equal to 0");
        }

        if (Decimal.Round(amount, 2) != amount)
        {
            throw new  ArgumentException("Amount must have max 2 decimals");
        }
    }

    #endregion

}