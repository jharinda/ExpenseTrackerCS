using ExpenseTracker.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerD6.Classes
{
    public class ShowSummaryReport
    {
        public ShowSummaryReport()
        {
            Console.WriteLine("Summary Report");
            Console.WriteLine("----------------");

            foreach (Category category in InMemory.user.Categories)
            {
                //double totalSpentInCategory = InMemory.user.Transactions
                //    .Where(t => t.Category == category.Type)
                //    .Sum(t => t.Amount);

                Console.WriteLine($"Category: {category.Name}");
                Console.WriteLine($"Budget: ${category.Budget}");
                //Console.WriteLine($"Total Spent: ${totalSpentInCategory}");
                //Console.WriteLine("----------------");
            }

            double overallBudget = InMemory.user.Categories.Sum(c => c.Budget);
            double overallSpending = InMemory.user.Transactions.Sum(t => t.Amount);

            Console.WriteLine("Overall Summary");
            Console.WriteLine($"Overall Budget: ${overallBudget}");
            Console.WriteLine($"Overall Spending: ${overallSpending}");
            Console.WriteLine("----------------");
        }
    }
}
