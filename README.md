# BankingServiceCodeTest
A simple banking service console applicatiom, implementing a simple CSV file based transaction processor. 

Each day companies provide a CSV file with transfers they want to make between accounts for customers they are doing business with.

## Functionalities:
1. Loads account balances from a CSV file
2. Loads transaction records from a CSV file
3. Processes transactions in order
4. Writes final balances after transactions to the console
5. Writes failures if any to the console

## Assumptions:
1. Account number must be 16 characters long.
2. Account balance must not go below 0.
3. Account balance will always be rounded off to 2 decimal places
5. Outputs/Errors are written to console
6. CSV files provided do not have any headers and rows are parsed from the first line
7. In-Memory dictionary/map used to store account information (No databases used at this stage)

## Error handling:
1. Availability of source and destination accounts
2. Insufficient funds
3. Data validation
4. Disregard incorrect number of columns in CSV files

## Tests:
1. Account validation including Debit&Credit methods
2. Account loading
3. Account repository behaviors
4. Transfer processor 

## High level architecture:
The project follows a layered domain model architecture:
1. Banking.Core - Business rules including the Core domain models
2. Banking.Application - Orchestrates the banking system
3. Banking.Infrastructure - Includes file readers and the repository layer
4. Program.cs - Entry point to the project

## Potential improvements
1. Wire up a database for persistent storage
2. Transaction level atomicity for processing
3. Add structured logging
4. Add a global exception handling mechanism
5. Add a reporting mechanism
6. Improve CSV validation and tests

## Technical specification
1. .NET 10
2. xUnit for testing (No mocking frameworks used at this stage)

## How to run
1. Navigate to the root directory of the project
2. The Data folder contains the Balances and Transactions CSV files. Please follow the same naming convention as the existing names in the folder.
3. Run the command "dotnet build" to build the project
4. Run the command "dotnet run" to run the banking service and view the output

   Sample output:

      Loaded accounts: 5
      Transactions processed: 4, succeeded: 4

      Transactions failed: 0

      Final balances:
      1111234522221234,9974.40
      1111234522226789,4820.50
      1212343433335665,1725.60
      2222123433331212,1550.00
      3212343433335755,48679.50
5. Run the command "dotnet test" to run the tests

