using ExpenseTracker.Classes;
using ExpenseTrackerD6.Classes;

namespace ExpenseTracker.Database
{
    class InMemory
    {
        public static User user = new User() 
        { 
            Id = Guid.NewGuid(), 
            Name = "Janith", 
            Transactions = new List<Transaction>(),
            Categories  = new List<Category>()
        };

        public static List<RecurringTransaction> recurringTransactions = new List<RecurringTransaction>();

        public static void addRecurringTransaction(Guid transactionId)
        {
            RecurringTransaction list = recurringTransactions.Find(rt => rt.transactionId == transactionId);
            if (list != null && list.transactionId != null) 
            {
                recurringTransactions.Add(new RecurringTransaction(user.Id, transactionId));
            }
        }
    }

    class RecurringTransaction
    {
        public RecurringTransaction(Guid _userId, Guid _transactionId)
        {
            userId = _userId;
            transactionId = _transactionId;
        }

        public Guid userId { get; set; }
        public Guid transactionId { get; set; }
    }
}
