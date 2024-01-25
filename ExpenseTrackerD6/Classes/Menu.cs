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
             InMemory.user.addTransaction("T1",100,"c",DateTime.Now,TransactionType.Income,TransactionCategory.Normal,true);
             InMemory.user.addTransaction("T2",200,"c1",DateTime.Now,TransactionType.Income,TransactionCategory.Normal,true);
            createMainMenu();
        }
        
        private void createMainMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Transactions");
                Console.WriteLine("2.Categories");
                Console.WriteLine("3.Show Summary Report");
                Console.WriteLine("4.Exit");

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
                        new ShowSummaryReport();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    case "5":
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
    }
}
