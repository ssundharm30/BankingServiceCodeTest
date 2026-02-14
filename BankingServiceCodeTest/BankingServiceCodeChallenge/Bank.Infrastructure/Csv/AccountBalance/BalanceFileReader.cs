using BankingServiceCodeTest.Bank.Core.Acccount;

namespace BankingServiceCodeTest.Bank.Infrastructure.Csv.AccountBalance;

/// <summary>
/// A CSV helper class specifically for the Balances CSV file.
/// It provides a reader specific for Balances file
/// </summary>
public class BalanceFileReader
{
    
    private readonly CsvReader _csvReader;
    
    public BalanceFileReader(CsvReader csvReader)
    {
        _csvReader = csvReader;
    }
    
    
    public IEnumerable<BalanceRow> Parse(string filePath)
    {
        foreach (var (rowNum, cols) in _csvReader.Read(filePath, hasHeader: false))
        {
            if (cols.Length < 2) continue;

            if (!decimal.TryParse(cols[1], out var bal))
                continue;
            var account = new Account(cols[0], bal);
            yield return (new BalanceRow()
            {
                BalanceRowId = rowNum,
                Account = account,
            });
        }
    }
}