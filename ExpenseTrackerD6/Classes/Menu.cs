using ExpenseTracker.Database;
using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTrackerD6.Classes
{
    public class Menu
    {
        private void CategoriesMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Create Category");
                Console.WriteLine("2.Edit Category");
                Console.WriteLine("3.View Category");
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
