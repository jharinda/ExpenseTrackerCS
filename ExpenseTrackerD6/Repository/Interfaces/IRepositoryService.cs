using System.Transactions;

namespace ExpenseTracker.Repository.Interfaces
{
    public interface IRepositoryService
    {
        public void addTransaction(Transaction transaction);
        public void editTransaction(Transaction transaction);
        public void deleteTransaction(Transaction transaction);
    }
}
