using ExpenseTracker.Database;
using ExpenseTracker.Enums;
using ExpenseTrackerD6.Classes;

namespace ExpenseTracker.Classes
{
    public class CategoryMenu
    {
        public CategoryMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Create Category");
                Console.WriteLine("2.Edit Category");
                Console.WriteLine("3.View Categories");
                Console.WriteLine("4.Delete Categories");
                Console.WriteLine("5.Back");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        createCategory();
                        break;
                    case "2":
                        editCategory();
                        break;
                    case "3":
                        viewCategory();
                        break;
                    case "4":
                        deleteCategory();
                        break;        
                    case "5":
                        return;
                    default:
                        invalidChoice();
                        break;
                }
            }
        }

        private void createCategory()
        {
            // name
            Console.Write("Enter Name: ");
            var nameStr = Console.ReadLine();


            while (nameStr.Equals(""))
            {
                Console.WriteLine("Invalid input. Try Again");
                nameStr = Console.ReadLine();
            }

            // type
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


            // Budget
            Console.Write("Enter Budget (Amount): ");
            var amountStr = Console.ReadLine();

            while (!TryParseDouble(amountStr, out double valAmount))
            {
                Console.WriteLine("Invalid input. Try Again");
                amountStr = Console.ReadLine();
            }

            TryParseDouble(amountStr, out double amountPrased);
            var amount = amountPrased;

            InMemory.user.addCategory(nameStr, transactionType, amount);

        }

        private void viewCategory()
        {
            Console.WriteLine("Available Categories");
            Console.WriteLine("+----+----------------------+------------+---------------------+");
            Console.WriteLine("| No |        Name          | Type       |  Budget   ");
            Console.WriteLine("+----+----------------------+------------+---------------------+");

            foreach (Category category in InMemory.user.Categories)
            {
                Console.WriteLine($"| {InMemory.user.Categories.IndexOf(category) + 1}  | {category.Name}            | {category.Type}   |  ${category.Budget}");
            }
            Console.WriteLine("+----+----------------------+------------+---------------------+");
        }

        private void editCategory()
        {
            viewCategory();
            Console.Write("Which Category you want to edit ? ");
            var indexStr = Console.ReadLine();
            int index = 0;

            while (!Int32.TryParse(indexStr, out index))
            {
                Console.WriteLine("Invalid input. Try Again");
                indexStr = Console.ReadLine();
            }

            while (InMemory.user.Categories.Count < index)
            {
                Console.WriteLine("Invalid selection. Try Again");
                indexStr = Console.ReadLine();
                while (!Int32.TryParse(indexStr, out index))
                {
                    Console.WriteLine("Invalid input. Try Again");
                    indexStr = Console.ReadLine();
                }
            }

            
            Category cat = InMemory.user.Categories[index - 1];

            // Name
            Console.Write($"Enter Name ( Old value : {cat.Name} ): ");
            var titleStr = Console.ReadLine();


            if (titleStr.Equals(""))
            {   
                titleStr = cat.Name;
            }
         

            // Type
            Console.Write($"Enter Type ( Old value : {cat.Type} ): ");
            var typeStr = Console.ReadLine();

            var transactionType = cat.Type;


            if (!typeStr.Trim().Equals(""))
            {
                while (!Enum.TryParse<TransactionType>(typeStr, true, out TransactionType valTransactionType))
                {
                    Console.WriteLine("Invalid input. Try Again");
                    typeStr = Console.ReadLine();
                }

                Enum.TryParse<TransactionType>(typeStr, true, out TransactionType tt);
                transactionType = tt;
            }
         
            // budget
            Console.Write($"Enter Budget ( Old value : {cat.Budget} ): ");
            var budgetStr = Console.ReadLine();

            var budget = cat.Budget;

            if (!budgetStr.Trim().Equals("")){

                while (!TryParseDouble(budgetStr, out double valAmount))
                {
                    Console.WriteLine("Invalid input. Try Again");
                    budgetStr = Console.ReadLine();
                }
                TryParseDouble(budgetStr, out double amountPrased);
                budget = amountPrased;
            }
            
 
            Category editedCategory = new Category(titleStr, transactionType, budget);

            editedCategory.Id = cat.Id;

            InMemory.user.updateCategory(editedCategory);

        }

        private void deleteCategory()
        {
            viewCategory();
            Console.Write("Which Category you want to delete ? ");
            var indexStr = Console.ReadLine();
            int index = 0;

            while (!Int32.TryParse(indexStr, out index))
            {
                Console.WriteLine("Invalid input. Try Again");
                indexStr = Console.ReadLine();
            }

            while (InMemory.user.Categories.Count < index)
            {
                Console.WriteLine("Invalid selection. Try Again");
                indexStr = Console.ReadLine();
                while (!Int32.TryParse(indexStr, out index))
                {
                    Console.WriteLine("Invalid input. Try Again");
                    indexStr = Console.ReadLine();
                }
            }


            Category cat = InMemory.user.Categories[index - 1];

            InMemory.user.deleteCategory(cat);
        }

        static bool TryParseDouble(string input, out double result)
        {
            return double.TryParse(input, out result);
        }

        private void invalidChoice()
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }
}
