using ExpenseTracker.Database;
using ExpenseTracker.Enums;
using ExpenseTracker.Repository;
using ExpenseTracker.Repository.Interfaces;
using ExpenseTrackerD6.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTracker.Classes
{
    public class Menu
    {
        public Menu()
        {
            InMemory.user.addCategory("Transport", TransactionType.Expense, 10000.00);
            InMemory.user.addCategory("Meal", TransactionType.Expense, 2500.00);
            InMemory.user.addCategory("Income", TransactionType.Income, 50000.00);
            InMemory.user.addCategory("Rent", TransactionType.Income, 15000.00);

            //InMemory.user.addTransaction("T1",100,"c",DateTime.Now,TransactionType.Income,null,true);
            // InMemory.user.addTransaction("T2",200,"c1",DateTime.Now,TransactionType.Income, null, true);
            createMainMenu();
        }
        
        private void createMainMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Transactions");
                Console.WriteLine("2.Categories");
                Console.WriteLine("3.Analysis");
                Console.WriteLine("3.Exit");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        new TransactionMenu();
                        break;
                    case "2":
                        new CategoryMenu();
                        break;
                    case "3":
                        new SummaryReport();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
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
    }
}
