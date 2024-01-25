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

        private void invalidChoice()
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }
}
