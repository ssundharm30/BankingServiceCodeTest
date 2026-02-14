using BankingServiceCodeTest.Bank.Infrastructure.Csv.Transactions;

namespace BankingServiceCodeTest.Bank.Application.Transaction;

public interface ITransactionService
{
    /// <summary>
    /// Processes transactions using the transaction records
    /// from the transactions csv file
    /// </summary>
    /// <param name="rows"></param>
    /// <returns>TransactionResult including failure reasons</returns>
    TransactionResult ProcessTransaction(IEnumerable<TransactionRow> rows);
}