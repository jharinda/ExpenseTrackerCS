using ExpenseTracker.Database;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Classes
{
    public class Menu
    {
        public Menu()
        {

            InMemory.user.addTransaction("T1",100,"c",DateTime.Now,TransactionType.Income,TransactionCategory.Normal,true);
            InMemory.user.addTransaction("T2",200,"c1",DateTime.Now,TransactionType.Income,TransactionCategory.Normal,true);
            createMainMenu();
            //createTransaction();
        }
        
        private void createMainMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Transactions");
                Console.WriteLine("2.Categories");
                Console.WriteLine("3.Exit");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        createTransactionMenu();
                        break;
                    case "2":
                        createCategoriesMenu();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        invalidChoice();
                        break;
                }
            }
        }

        private void createTransactionMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Create Transaction");
                Console.WriteLine("2.Edit Transaction");
                Console.WriteLine("3.Delete Transaction");
                Console.WriteLine("4.View Transactions");
                Console.WriteLine("5.Back");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        createTransaction();
                        break;
                    case "2":
                        editTransaction();
                        break;
                    case "3":
                        deleteTransaction();
                        break;
                    case "4":
                        viewTransactions();
                        break;
                    case "5": 
                        return;
                    default:
                        invalidChoice();
                        break;
                }

            }

        }

        private void deleteTransaction()
        {
            viewTransactions();

            var index = Console.ReadLine();

            while (!Int32.TryParse(index, out int i))
            {
                Console.WriteLine("Invalid input. Try Again");
                index = Console.ReadLine();
            }

            Int32.TryParse(index, out int prasedIndex);

            InMemory.user.deleteTransaction(InMemory.user.Transactions[prasedIndex - 1]);
        }

        private static void viewTransactions()
        {
            Console.WriteLine("Available Transactions");
            foreach (Transaction transaction in InMemory.user.Transactions)
            {
                Console.WriteLine($"{InMemory.user.Transactions.IndexOf(transaction) + 1} - {transaction.Title} - {transaction.Amount} - {transaction.Type} - {transaction.Category}");
            }
        }

        private void editTransaction()
        {
            viewTransactions();
        }

        static bool TryParseDouble(string input, out double result)
        {
            return double.TryParse(input, out result);
        }

        private void createTransaction()
        {
            // Title
            Console.Write("Enter Title: ");
            var titleStr = Console.ReadLine();

            // Amount
            Console.Write("Enter Amount: ");
            var amountStr = Console.ReadLine();

            while (!TryParseDouble(amountStr, out double valAmount))
            {
                Console.WriteLine("Invalid input. Try Again");
                amountStr = Console.ReadLine();
            }

            TryParseDouble(amountStr, out double amountPrased);
            var amount = amountPrased;

            // Comment
            Console.Write("Enter Comment: ");
            var comment = Console.ReadLine();

            // Date
            Console.Write("Enter Date (yyyy-mm-dd): ");
            var dateStr = Console.ReadLine();

            while (!DateTime.TryParse(dateStr, out DateTime valDate))
            {
                Console.WriteLine("Invalid input. Try Again");
                dateStr = Console.ReadLine();
            }

            DateTime.TryParse(dateStr, out DateTime datePrased);
            var date = datePrased;

            // Date
            Console.WriteLine("Enter Type : (Type the value)");
            foreach (TransactionType type in Enum.GetValues(typeof(TransactionType)))
            {
                Console.WriteLine($"- {type}");
            }

            var typeStr = Console.ReadLine();

            while (!Enum.TryParse<TransactionType>(typeStr, true, out TransactionType valTransactionType))
            {
                Console.WriteLine("Invalid input. Try Again");
                typeStr = Console.ReadLine();
            }

            Enum.TryParse<TransactionType>(typeStr, true, out TransactionType tt);
            var transactionType = tt;

            // Category
            Console.WriteLine("Enter Category : (Type the value)");
           
            foreach (TransactionCategory type in Enum.GetValues(typeof(TransactionCategory)))
            {
                Console.WriteLine($"- {type}");
            }

            var categoryStr = Console.ReadLine();


            while (!Enum.TryParse<TransactionCategory>(categoryStr, true, out TransactionCategory valTransactionType))
            {
                Console.WriteLine("Invalid input. Try Again");
                categoryStr = Console.ReadLine();
            }

            Enum.TryParse<TransactionCategory>(typeStr, true, out TransactionCategory tc);
            var transactionCategory = tc;

            // Recurring
            Console.Write("Is this Transaction Recurring (y/n): ");
            var isRecurringStr = Console.ReadLine();

            while (isRecurringStr != "y" && isRecurringStr != "n") 
            {
                Console.WriteLine("Invalid input. Try Again");
                isRecurringStr = Console.ReadLine();
            }

            bool isRecurring = isRecurringStr == "y" ? true : false;

            InMemory.user.addTransaction(titleStr, amount,comment,date, transactionType, transactionCategory, isRecurring);

        }

        private void createCategoriesMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Create Category");
                Console.WriteLine("2.Edit Category");
                Console.WriteLine("3.View Transactions");
                Console.WriteLine("4.Back");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        return;
                    default:
                        invalidChoice();
                        break;
                }
            }
        }

        private void invalidChoice() {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }
}
