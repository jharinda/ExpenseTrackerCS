using ExpenseTracker.Database;
using ExpenseTracker.Enums;
using ExpenseTracker.Repository.Interfaces;
using ExpenseTrackerD6.Classes;
using System;
using System.Data.Common;
using System.Transactions;

namespace ExpenseTracker.Classes
{
    public class TransactionMenu
    {
        public TransactionMenu()
        {
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
        }
        private void invalidChoice()
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }

        private void deleteTransaction()
        {
            if (InMemory.user.Transactions.Count > 0)
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
            else
            {
                Console.WriteLine("No transcations");
            }
        }

        private static void viewTransactions()
        {

            Console.WriteLine("Available Transactions");
            Console.WriteLine("+----+----------------------+------------+--------+-------------+----------------+---------------------+");
            Console.WriteLine("| No |        Title         |   Amount   |  Type  |  Category   | Is Recurring   |        Date         |");
            Console.WriteLine("+----+----------------------+------------+--------+-------------+----------------+---------------------+");

            //Console.WriteLine("No - Title - Amount - Type - Category - Is Recurring - Date");
            foreach (Transaction transaction in InMemory.user.Transactions)
            {
                Console.WriteLine($"| {InMemory.user.Transactions.IndexOf(transaction) + 1}  | {transaction.Title}      | ${transaction.Amount}   | {transaction.Type} | {transaction.Category.Name} | {transaction.IsRecurring}             | {transaction.Date} |");
            }
            Console.WriteLine("+----+----------------------+------------+--------+-------------+----------------+---------------------+");
        }
        private void editTransaction()
        {
            if (InMemory.user.Transactions.Count > 0)
            {
            
            viewTransactions();        
            Console.Write("Which transition you want to edit ? ");
            var indexStr = Console.ReadLine();

            while (!Int32.TryParse(indexStr, out int ind))
            {
                Console.WriteLine("Invalid input. Try Again");
                indexStr = Console.ReadLine();
            }

            Int32.TryParse(indexStr, out int i);

            Transaction tr = InMemory.user.Transactions[i - 1];

            // Title
            Console.Write($"Enter Title ( Old value : {tr.Title} ): ");
            var titleStr = Console.ReadLine();

            // Amount
            Console.Write($"Enter Amount ( Old value : {tr.Amount} ): ");
            var amountStr = Console.ReadLine();

            while (!TryParseDouble(amountStr, out double valAmount))
            {
                Console.WriteLine("Invalid input. Try Again");
                amountStr = Console.ReadLine();
            }

            TryParseDouble(amountStr, out double amountPrased);
            var amount = amountPrased;

            // Comment
            Console.Write($"Enter Comment ( Old value : {tr.Comment} ): ");
            var comment = Console.ReadLine();

            // Date
            Console.Write($"Enter Date ( Old value : {tr.Date} ) (yyyy-mm-dd): ");
            var dateStr = Console.ReadLine();

            while (!DateTime.TryParse(dateStr, out DateTime valDate))
            {
                Console.WriteLine("Invalid input. Try Again");
                dateStr = Console.ReadLine();
            }

            DateTime.TryParse(dateStr, out DateTime datePrased);
            var date = datePrased;

            // Date
            Console.WriteLine($"Enter Type  ( Old value : {tr.Type} ) : (Type the value)");
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
            /*
            Console.WriteLine($"Enter Category  ( Old value : {tr.Category.Name} ): (Type the value)");

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
            */

            viewCategory(transactionType);

            Console.WriteLine("Select the category");
            var catStr = Console.ReadLine();
            int index = 0;

            while (!Int32.TryParse(catStr, out index))
            {
                Console.WriteLine("Invalid input. Try Again");
                catStr = Console.ReadLine();
            }

            while (InMemory.user.Categories.Count < index || InMemory.user.Categories[index - 1].Type != transactionType)
            {
                Console.WriteLine("Invalid selection. Try Again");
                catStr = Console.ReadLine();
                while (!Int32.TryParse(catStr, out index))
                {
                    Console.WriteLine("Invalid input. Try Again");
                    catStr = Console.ReadLine();
                }

                    while (InMemory.user.Categories[index - 1].Type != transactionType)
                    {
                        Console.WriteLine("Invalid input. Try Again");
                        catStr = Console.ReadLine();
                    }
                }

       

            Category cat = InMemory.user.Categories[index - 1];

            // Recurring
            Console.Write($"Is this Transaction Recurring  ( Old value : {tr.IsRecurring} ) (y/n): ");
            var isRecurringStr = Console.ReadLine();

            while (isRecurringStr != "y" && isRecurringStr != "n")
            {
                Console.WriteLine("Invalid input. Try Again");
                isRecurringStr = Console.ReadLine();
            }

            bool isRecurring = isRecurringStr == "y" ? true : false;

            Transaction editedTransaction = new Transaction(titleStr, amount, comment, date, transactionType, cat, isRecurring);

            editedTransaction.Id = tr.Id;

            InMemory.user.updateTransaction(editedTransaction);

            if (isRecurring)
            {
                InMemory.addRecurringTransaction(editedTransaction.Id);
            }

          }
            else
            {
                Console.WriteLine("No transcations");
            }
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
            /*
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
            */

            viewCategory(transactionType);

            Console.WriteLine("Select the category");
            var catStr = Console.ReadLine();
            int index = 0;

            while (!Int32.TryParse(catStr, out index))
            {
                Console.WriteLine("Invalid input. Try Again");
                catStr = Console.ReadLine();
            }

            while (InMemory.user.Categories.Count < index || InMemory.user.Categories[index - 1].Type != transactionType)
            {
                Console.WriteLine("Invalid selection. Try Again");
                catStr = Console.ReadLine();
                while (!Int32.TryParse(catStr, out index))
                {
                    Console.WriteLine("Invalid input. Try Again");
                    catStr = Console.ReadLine();
                }

            }

            Category cat = InMemory.user.Categories[index - 1];



            // Recurring
            Console.Write("Is this Transaction Recurring (y/n): ");
            var isRecurringStr = Console.ReadLine();

            while (isRecurringStr != "y" && isRecurringStr != "n")
            {
                Console.WriteLine("Invalid input. Try Again");
                isRecurringStr = Console.ReadLine();
            }

            bool isRecurring = isRecurringStr == "y" ? true : false;

            Transaction t = InMemory.user.addTransaction(titleStr, amount, comment, date, transactionType, cat, isRecurring);

            if (isRecurring)
            {
                InMemory.addRecurringTransaction(t.Id);
            }

        }

        private void viewCategory(TransactionType type)
        {
            Console.WriteLine("Available Categories");
            Console.WriteLine("+----+----------------------+------------+---------------------+");
            Console.WriteLine("| No |        Name          | Type       |  Budget   ");
            Console.WriteLine("+----+----------------------+------------+---------------------+");

            foreach (Category category in InMemory.user.viewCategory(type))
            {
                Console.WriteLine($"| {InMemory.user.Categories.IndexOf(category) + 1}  | {category.Name}           | {category.Type}   |  ${category.Budget}");
            }
            Console.WriteLine("+----+----------------------+------------+---------------------+");
        }
    }
}
