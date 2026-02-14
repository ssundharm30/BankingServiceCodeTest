using BankingServiceCodeTest.Bank.Infrastructure.Csv.Transactions;
using BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;

namespace BankingServiceCodeTest.Bank.Application.Transaction;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepository _accountRepository;
    
    public TransactionService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public TransactionResult ProcessTransaction(IEnumerable<TransactionRow> rows)
    {
        var result = new TransactionResult();

        foreach (var row in rows)
        {
            result.Processed++;

            var fromAccount = _accountRepository.GetAccountByAccountNumber(row.Transaction.FromAccountNumber);
            if (fromAccount == null)
            {
                result.Failed.Add(new TransactionFailure
                {
                    RowNumber = row.TransactionRowId,
                    Reason = "Source account not found"
                });
                continue;
            }
            
            var toAccount = _accountRepository.GetAccountByAccountNumber(row.Transaction.ToAccountNumber);
            if (toAccount == null)
            {
                result.Failed.Add(new TransactionFailure
                {
                     RowNumber = row.TransactionRowId,
                     Reason = "Destination account not found"
                });
                continue;
            }
            
            try
            {
                fromAccount.Debit(row.Transaction.Amount);
                toAccount.Credit(row.Transaction.Amount);
                
                _accountRepository.Upsert(fromAccount);
                _accountRepository.Upsert(toAccount);
                result.Succeeded++;
            }
            catch (Exception ex)
            {
                result.Failed.Add(new TransactionFailure
                {
                    RowNumber = row.TransactionRowId,
                    Reason = $"Transaction failed due to {ex.Message}"
                });
            }
        }
        
        return result;
    }
}