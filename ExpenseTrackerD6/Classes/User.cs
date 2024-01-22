using ExpenseTracker.Enums;

namespace ExpenseTracker.Classes
{
    class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }

        public void addTransaction(string title, double amount, string comment,DateTime date, TransactionType type, TransactionCategory category, bool isRecurring)
        {
            Transaction t = new Transaction(title,amount,comment,date,type,category,isRecurring);
            Transactions.Add(t);
            sortRecords();
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


    }

  
}
