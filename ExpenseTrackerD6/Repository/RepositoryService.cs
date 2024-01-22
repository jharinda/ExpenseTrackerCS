using ExpenseTracker.Classes;
using ExpenseTracker.Database;

namespace ExpenseTracker.Repository
{
    class RepositoryService
    {
        public RepositoryService()
        {
            CurrentUser = InMemory.user;
        }
        public User CurrentUser { get; set; }
    }
}
