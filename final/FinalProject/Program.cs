using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceManager
{
    public class Transaction
    {
        public int Id { get; set; }
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
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }

        public User()
        {
            Transactions = new List<Transaction>();
        }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your SQL Server connection here
            optionsBuilder.UseSqlServer("Server=localhost;Database=PersonalFinanceManager;Trusted_Connection=True;");
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

            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Name == userName);
                if (user == null)
                {
                    user = new User { Name = userName };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                CurrentUser = user;
            }

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

            var transaction = new Transaction
            {
                Amount = amount,
                Category = category,
                Type = transactionType,
                Date = DateTime.Now
            };

            using (var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Transactions).FirstOrDefault(u => u.Id == CurrentUser.Id);
                if (user != null)
                {
                    user.Transactions.Add(transaction);
                    context.SaveChanges();
                }
            }

            Console.WriteLine("Transaction added successfully!");
        }

        private void ViewFinancialStatus()
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Transactions).FirstOrDefault(u => u.Id == CurrentUser.Id);
                if (user != null)
                {
                    decimal totalExpenses = 0;
                    decimal totalIncome = 0;

                    foreach (var transaction in user.Transactions)
                    {
                        if (transaction.Type == TransactionType.Expense)
                        {
                            totalExpenses += transaction.Amount;
                        }
                        else if (transaction.Type == TransactionType.Income)
                        {
                            totalIncome += transaction.Amount;
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Financial Status:");
                    Console.WriteLine("Total Expenses: $" + totalExpenses);
                    Console.WriteLine("Total Income: $" + totalIncome);
                }
            }
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
            using (var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Transactions).FirstOrDefault(u => u.Id == CurrentUser.Id);
                if (user != null)
                {
                    Console.WriteLine("Monthly Expense Summary:");

                    // Get the current month and year
                    DateTime currentDate = DateTime.Now;
                    int currentMonth = currentDate.Month;
                    int currentYear = currentDate.Year;

                    decimal totalExpenses = 0;

                    foreach (var transaction in user.Transactions)
                    {
                        if (transaction.Type == TransactionType.Expense && transaction.Date.Month == currentMonth && transaction.Date.Year == currentYear)
                        {
                            totalExpenses += transaction.Amount;
                        }
                    }

                    Console.WriteLine($"Total Expenses for {currentMonth}/{currentYear}: ${totalExpenses}");
                }
            }
        }

        private void GenerateCategoryWiseSpendingTrends()
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Transactions).FirstOrDefault(u => u.Id == CurrentUser.Id);
                if (user != null)
                {
                    Console.WriteLine("Category-wise Spending Trends:");

                    var expensesByCategory = new Dictionary<string, decimal>();

                    foreach (var transaction in user.Transactions)
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

                    foreach (var category in expensesByCategory)
                    {
                        Console.WriteLine($"{category.Key}: ${category.Value}");
                    }
                }
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            PersonalFinanceApp app = new PersonalFinanceApp();
            app.Start();
        }
    }
}
