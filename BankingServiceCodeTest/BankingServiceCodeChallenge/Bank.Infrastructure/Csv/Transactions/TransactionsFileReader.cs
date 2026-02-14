using BankingServiceCodeTest.Bank.Csv.Transaction;

namespace BankingServiceCodeTest.Bank.Infrastructure.Csv.Transactions;

/// <summary>
/// A CSV helper class specifically for the Transactions CSV file.
/// It provides a reader specific for Transactions file
/// </summary>
public class TransactionsFileReader
{
    private readonly CsvReader _csvReader;
    
    public TransactionsFileReader(CsvReader csvReader)
    {
        _csvReader = csvReader;
    }
    
    public IEnumerable<TransactionRow> Parse(string filePath)
    {
        foreach (var (rowNum, cols) in _csvReader.Read(filePath, hasHeader: false))
        {
            if (cols.Length < 3) continue;

            if (!decimal.TryParse(cols[2], out var amount))
                continue;
            
            var transaction = new Transaction(cols[0], cols[1], amount);
            yield return (new TransactionRow()
            {
                TransactionRowId =  rowNum,
                Transaction = transaction
            });
        }
    }
}