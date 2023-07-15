Personal Finance Manager

---

Overview:
The Personal Finance Manager is a console application that helps you track your finances. It allows you to add transactions, view your financial status, and generate reports.

Prerequisites:
- .NET Core SDK (https://dotnet.microsoft.com/download)

Getting Started:
1. Clone the repository or download the source code.

2. Open a terminal or command prompt and navigate to the project directory.

3. Restore the project dependencies by running the following command:

4. Install the Entity Framework Core SQL Server package by running the following command:

5. Set up the SQL Server connection:
- Open the `AppDbContext.cs` file located in the `PersonalFinanceManager` folder.
- In the `OnConfiguring` method, update the connection string to match your SQL Server configuration.

6. Build the project by running the following command:

7. Create the initial database by running the following command:

8. Run the application by executing the following command:

Usage:
- When the application starts, enter your name to log in or create a new user.

- Choose from the following options:
- Add a transaction: Enter the transaction type (1 for Expense, 2 for Income), amount, and category.
- View financial status: Displays the total expenses and total income.
- Generate reports: Choose between a monthly expense summary and category-wise spending trends.
- Exit: Quit the application.

---


Happy tracking your finances with the Personal Finance Manager!

