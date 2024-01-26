using ExpenseTracker.Database;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerD6.Classes
{
    public class SummaryReport
    {
        public SummaryReport() 
        {
            while (true)
            {
                Console.WriteLine("1.Spending against overall budget");
                Console.WriteLine("2.Back");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        getOverallBudget();
                        break;
                    case "2":
                        return;
                    default:
                        invalidChoice();
                        break;
                }
            }
        }

        private void invalidChoice()
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }

        private void getOverallBudget()
        {
            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
            Console.WriteLine("|       Category       |  Type  |   Budget   | Actual |   Difference   |");
            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");

            foreach (Category category in InMemory.user.Categories)
            {
                if (category.Type == TransactionType.Expense) 
                { 
                double totalSpentInCategory = InMemory.user.Transactions
                    .Where(t => t.Category.Id == category.Id)
                    .Sum(t => t.Amount);

                double difference = category.Budget - totalSpentInCategory;

                Console.WriteLine($"| {category.Name,-20} | {category.Type,-6} | ${category.Budget,-11} | ${totalSpentInCategory,-6} | ${difference,-14} |");
                }
            }

            double overallExpenseBudget = InMemory.user.Categories.Sum(c => {
                if (c.Type == TransactionType.Expense) {
                   return c.Budget;
                }
                return 0;
            });
            double overallExpenseSpending = InMemory.user.Transactions.Sum(t => t.Amount);
            double overallExpenseDifference = overallExpenseBudget - overallExpenseSpending;

            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
            Console.WriteLine($"| Overall Summary      |        | ${overallExpenseBudget,-11} | ${overallExpenseSpending,-6} | ${overallExpenseDifference,-14} |");
            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
            Console.WriteLine("");

            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
            Console.WriteLine("|       Category       |  Type  |   Budget   | Actual |   Difference   |");
            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");

            foreach (Category category in InMemory.user.Categories)
            {
                if (category.Type == TransactionType.Income)
                {
                    double totalSpentInCategory = InMemory.user.Transactions
                        .Where(t => t.Category.Id == category.Id)
                        .Sum(t => t.Amount);

                    double difference = category.Budget - totalSpentInCategory;

                    Console.WriteLine($"| {category.Name,-20} | {category.Type,-6} | ${category.Budget,-11} | ${totalSpentInCategory,-6} | ${difference,-14} |");
                }
            }

            double overallIncomeBudget = InMemory.user.Categories.Sum(c => {
                if (c.Type == TransactionType.Income)
                {
                    return c.Budget;
                }
                return 0;
            });
            double overallIncomeSpending = InMemory.user.Transactions.Sum(t => {
                if (t.Type == TransactionType.Income)
                {
                    return t.Amount;
                }
                return 0;
            });
            double overallIncomeDifference = overallIncomeBudget - overallIncomeSpending;

            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
            Console.WriteLine($"| Overall Summary      |        | ${overallIncomeBudget,-11} | ${overallIncomeSpending,-6} | ${overallIncomeDifference,-14} |");
            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
        }
    }
}
