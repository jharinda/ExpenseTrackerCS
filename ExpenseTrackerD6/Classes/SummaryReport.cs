using ExpenseTracker.Database;
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
                double totalSpentInCategory = InMemory.user.Transactions
                    .Where(t => t.Category.Id == category.Id)
                    .Sum(t => t.Amount);

                double difference = category.Budget - totalSpentInCategory;

                Console.WriteLine($"| {category.Name,-20} | {category.Type,-6} | ${category.Budget,-11} | ${totalSpentInCategory,-6} | ${difference,-14} |");
            }

            double overallBudget = InMemory.user.Categories.Sum(c => c.Budget);
            double overallSpending = InMemory.user.Transactions.Sum(t => t.Amount);
            double overallDifference = overallBudget - overallSpending;

            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
            Console.WriteLine($"| Overall Summary      |        | ${overallBudget,-11} | ${overallSpending,-6} | ${overallDifference,-14} |");
            Console.WriteLine("+----------------------+--------+------------+--------+----------------+");
        }
    }
}
