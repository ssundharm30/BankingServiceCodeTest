using BankingServiceCodeTest.Bank.Application.Account;
using BankingServiceCodeTest.Bank.Application.Transaction;
using BankingServiceCodeTest.Bank.Infrastructure.Csv;
using BankingServiceCodeTest.Bank.Infrastructure.Csv.AccountBalance;
using BankingServiceCodeTest.Bank.Infrastructure.Csv.Transactions;
using BankingServiceCodeTest.Bank.Infrastructure.Repositories.Account;
using Microsoft.Extensions.DependencyInjection;
var services = new ServiceCollection();

// Infrastructure
services.AddSingleton<IAccountRepository, AccountRepository>();
services.AddSingleton<CsvReader>();
services.AddSingleton<BalanceFileReader>();
services.AddSingleton<TransactionsFileReader>();

// Application
services.AddSingleton<IAccountService, AccountService>();
services.AddSingleton<ITransactionService, TransactionService>();

var sp = services.BuildServiceProvider();

var baseDir = AppContext.BaseDirectory;

// go up to solution root
var solutionRoot = Path.GetFullPath(
    Path.Combine(baseDir, "..", "..", "..", "..")
);

var balancesPath = Path.Combine(solutionRoot, "Data", "mable_account_balances.csv");
var transactionsPath = Path.Combine(solutionRoot, "Data", "mable_transactions.csv");

var balanceFileReader = sp.GetRequiredService<BalanceFileReader>();
var transactionsFileReader = sp.GetRequiredService<TransactionsFileReader>();
var accountService = sp.GetRequiredService<IAccountService>();
var transactionService = sp.GetRequiredService<ITransactionService>();
var accountRepository = sp.GetRequiredService<IAccountRepository>();

var balances = balanceFileReader.Parse(balancesPath);
var loadedAccounts = accountService.LoadAccounts(balances);

var transactions = transactionsFileReader.Parse(transactionsPath);
var result = transactionService.ProcessTransaction(transactions);

Console.WriteLine($"Loaded accounts: {loadedAccounts}");
Console.WriteLine($"Transactions processed: {result.Processed}, succeeded: {result.Succeeded}");
Console.WriteLine();
Console.WriteLine($"Transactions failed: {result.Failed.Count}");
foreach(var failed in result.Failed)
{
    Console.WriteLine($"Transaction {failed.RowNumber} failed due to {failed.Reason}");
}
Console.WriteLine();
Console.WriteLine("Final balances:");
foreach (var a in accountRepository.GetAllAccounts().OrderBy(a => a.AccountNumber))
    Console.WriteLine($"{a.AccountNumber},{a.Balance}");