using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerD6.Classes
{
    class Category
    {
        public Category( string name, TransactionType type, double budget) {

            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            Budget = budget;
        }

        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }

    }
}
