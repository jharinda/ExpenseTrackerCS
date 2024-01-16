using ExpenseTracker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Database
{
    class InMemory
    {
        public static User user = new User() { Id = 0, Name = "Janith", Transactions = new List<Transaction>()};
    }
}
