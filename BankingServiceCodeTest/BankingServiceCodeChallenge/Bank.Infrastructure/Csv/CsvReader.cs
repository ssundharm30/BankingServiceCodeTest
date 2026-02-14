namespace BankingServiceCodeTest.Bank.Infrastructure.Csv;

/// <summary>
/// Generic CSV file reader that returns row number
/// and an array of the columns from a CSV file
/// </summary>
public class CsvReader
{
    public IEnumerable<(int RowNumber, string[] Columns)> Read(string path, bool hasHeader = true)
    {
        using var reader = new StreamReader(path);
        string? line;
        int row = 0;

        while ((line = reader.ReadLine()) != null)
        {
            row++;
            if (row == 1 && hasHeader) continue;
            if (string.IsNullOrWhiteSpace(line)) continue;

            yield return (row, line.Split(',').Select(x => x.Trim()).ToArray());
        }
    }
}