using ExpenseTracker.Enums;
using ExpenseTrackerD6.Classes;

namespace ExpenseTracker.Classes
{
    class Transaction
    {
        public Transaction(string title, double amount, string comment, DateTime date, TransactionType type, Category category, bool isRecurring)
        {
            Id = Guid.NewGuid();
            Title = title;
            Amount = amount;
            Comment = comment;
            Date = date;
            Type = type;
            Category = category;
            IsRecurring = isRecurring;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public Category Category { get; set; }
        public bool IsRecurring { get; set; }
    }
}
