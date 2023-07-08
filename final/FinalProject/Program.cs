using System;
using System.Collections.Generic;

namespace PersonalFinanceManager
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public TransactionType Type { get; set; }

         public DateTime Date { get; set; }
    }

    public enum TransactionType
    {
        Expense,
        Income
    }

    public class User
    {
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }

        public User(string name)
        {
            Name = name;
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(decimal amount, string category, TransactionType type)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                Category = category,
                Type = type,
                Date = DateTime.Now 
            };

            Transactions.Add(transaction);
        }

        public decimal CalculateTotalExpenses()
        {
            decimal totalExpenses = 0;

            foreach (var transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Expense)
                {
                    totalExpenses += transaction.Amount;
                }
            }

            return totalExpenses;
        }

        public decimal CalculateTotalIncome()
        {
            decimal totalIncome = 0;

            foreach (var transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Income)
                {
                    totalIncome += transaction.Amount;
                }
            }

            return totalIncome;
        }

        public Dictionary<string, decimal> CalculateExpensesByCategory()
        {
            var expensesByCategory = new Dictionary<string, decimal>();

            foreach (var transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Expense)
                {
                    if (expensesByCategory.ContainsKey(transaction.Category))
                    {
                        expensesByCategory[transaction.Category] += transaction.Amount;
                    }
                    else
                    {
                        expensesByCategory.Add(transaction.Category, transaction.Amount);
                    }
                }
            }

            return expensesByCategory;
        }
    }

    public class PersonalFinanceApp
    {
        public User CurrentUser { get; set; }

        public void Start()
        {
            Console.WriteLine("Welcome to Personal Finance Manager!");

            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();

            CurrentUser = new User(userName);

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("1. Add a transaction");
                Console.WriteLine("2. View financial status");
                Console.WriteLine("3. Generate reports");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTransaction();
                        break;
                    case "2":
                        ViewFinancialStatus();
                        break;
                    case "3":
                        GenerateReports();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Thank you for using Personal Finance Manager. Goodbye!");
        }

        private void AddTransaction()
        {
            Console.WriteLine();
            Console.WriteLine("1. Expense");
            Console.WriteLine("2. Income");
            Console.WriteLine();
            Console.Write("Enter the transaction type: ");
            string typeChoice = Console.ReadLine();

            TransactionType transactionType;
            if (typeChoice == "1")
            {
                transactionType = TransactionType.Expense;
            }
            else if (typeChoice == "2")
            {
                transactionType = TransactionType.Income;
            }
            else
            {
                Console.WriteLine("Invalid transaction type. Please try again.");
                return;
            }

            Console.Write("Enter the amount: ");
            string amountInput = Console.ReadLine();

            decimal amount;
            if (!decimal.TryParse(amountInput, out amount))
            {
                Console.WriteLine("Invalid amount. Please try again.");
                return;
            }

            Console.Write("Enter the category: ");
            string category = Console.ReadLine();

            CurrentUser.AddTransaction(amount, category, transactionType);

            Console.WriteLine("Transaction added successfully!");
        }

        private void ViewFinancialStatus()
        {
            Console.WriteLine();
            Console.WriteLine("Financial Status:");
            Console.WriteLine("Total Expenses: $" + CurrentUser.CalculateTotalExpenses());
            Console.WriteLine("Total Income: $" + CurrentUser.CalculateTotalIncome());
        }

        private void GenerateReports()
        {
            Console.WriteLine();
            Console.WriteLine("1. Monthly Expense Summary");
            Console.WriteLine("2. Category-wise Spending Trends");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            string reportChoice = Console.ReadLine();

            switch (reportChoice)
            {
                case "1":
                    GenerateMonthlyExpenseSummary();
                    break;
                case "2":
                    GenerateCategoryWiseSpendingTrends();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void GenerateMonthlyExpenseSummary()
        {
            Console.WriteLine("Monthly Expense Summary:");

            // Get the current month and year
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;

            // Filter transactions for the current month and year
            List<Transaction> monthlyTransactions = CurrentUser.Transactions.FindAll(
                t => t.Type == TransactionType.Expense &&
                t.Date.Month == currentMonth &&
                t.Date.Year == currentYear
            );

            decimal totalExpenses = 0 ;

            foreach (var transaction in monthlyTransactions)
            {
                totalExpenses += transaction.Amount;
            }

            Console.WriteLine($"Total Expenses for {currentMonth}/{currentYear}: ${totalExpenses}");
        }

        private void GenerateCategoryWiseSpendingTrends()
        {
            Console.WriteLine("Category-wise Spending Trends:");

            // Gather expenses by category
            Dictionary<string, decimal> expensesByCategory = CurrentUser.CalculateExpensesByCategory();

            foreach (var category in expensesByCategory)
            {
                Console.WriteLine($"{category.Key}: ${category.Value}");
            }
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            PersonalFinanceApp app = new PersonalFinanceApp();
            app.Start();
        }
    }
}
