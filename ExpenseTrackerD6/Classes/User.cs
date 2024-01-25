using ExpenseTracker.Enums;
using ExpenseTrackerD6.Classes;

namespace ExpenseTracker.Classes
{
    class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }

        public List<Category> Categories { get; set; }

        public Transaction addTransaction(string title, double amount, string comment,DateTime date, TransactionType type, TransactionCategory category, bool isRecurring)
        {
            Transaction t = new Transaction(title,amount,comment,date,type,category,isRecurring);
            Transactions.Add(t);
            sortRecords();
            return t;
        }

        public void sortRecords()
        {
            Transactions.Sort((x, y) => y.Date.CompareTo(x.Date));
            Transactions.Reverse();
        }

        public void updateTransaction(Transaction transaction)
        {
            Transaction selectedTransaction = Transactions.Find(r => r.Id == transaction.Id);

            if (selectedTransaction != null) 
            {
                Transactions.Remove(selectedTransaction);
                Transactions.Add(transaction);

                sortRecords();
            }
            else
            {
                Console.WriteLine("Error: Record not found");
            }


        }

        public void deleteTransaction(Transaction transaction)
        {
            Transaction selectedTransaction = Transactions.Find(r => r.Id == transaction.Id);

            if (selectedTransaction != null)
            {
                Transactions.Remove(selectedTransaction);
            }
            else
            {
                Console.WriteLine("Error: Record not found");
            }

        }

        public void addCategory(string name, TransactionType type, double budget)
        {
            Category cat = new Category(name, type, budget);
            Categories.Add(cat);
            Console.WriteLine("Category Created");
        }

        public List<Category> viewCategory(TransactionType type)
        {
            List<Category> categories = Categories.FindAll(r => r.Type == type);
            return categories;
        }

        public void deleteCategory(Category category)
        {
            Category selectedCategory = Categories.Find(r => r.Id == category.Id);

            if (selectedCategory != null)
            {
                Categories.Remove(selectedCategory);
                Console.WriteLine("Category Deleted!");
            }
            else
            {
                Console.WriteLine("Error: Record not found");
            }
        }

        public void updateCategory(Category category)
        {
            Category selectedCategory = Categories.Find(r => r.Id == category.Id);

            if (selectedCategory != null)
            {
                Categories.Remove(selectedCategory);
                Categories.Add(category);

                sortRecords();
            }
            else
            {
                Console.WriteLine("Error: Record not found");
            }


        }

    }

  
}
