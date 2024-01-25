using ExpenseTracker.Database;
using ExpenseTracker.Enums;
using ExpenseTracker.Repository;
using ExpenseTracker.Repository.Interfaces;
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
                        Environment.Exit(0);
                        break;
                    case "4":
                        return;
                    default:
                        invalidChoice();
                        break;
                }
            }
        }

        private void createCategory()
        {
            try
            {
                Console.Write("Enter Category Name: ");
                var name = Console.ReadLine();

                if(name.Equals("")) 
                {
                    throw new Exception("Invalid Category Name");
        }

                Console.Write("Enter Category Type (Income,Expense): ");

                var type = Console.ReadLine();

                if (name.Equals(""))
                {
                    throw new Exception("Invalid Category Type");
                }

                if(!Enum.IsDefined(typeof(TransactionType), type))
                {
                    throw new Exception("Invalid Category Type");
                }

                InMemory.user.addCategory(name, ParseEnum<TransactionType>(type) );

            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void editCategory()
        {
           
        }

        private void viewCategory()
        {
            List<Category> categories;
            categories = InMemory.user.viewCategory(TransactionType.Income);
            Console.Write("---Income Categories---");
            foreach (Category category in categories)
            {
                Console.WriteLine(category.Name);
            }
            Console.Write("");
            Console.Write("---Expense Categories---");
            categories = InMemory.user.viewCategory(TransactionType.Expense);
            foreach (Category category in categories)
            {

            }

        }

            public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private void invalidChoice() 
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }
}
