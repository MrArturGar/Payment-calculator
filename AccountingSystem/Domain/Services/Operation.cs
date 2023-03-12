using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Data
{
    public class Operation :IOperation
    {
        public Operation(int id, double amount, DateTime date, AccountType accountType)
        {
            Id = id;
            Amount = amount;
            Date = date;
            AccountType = accountType;
        }

        public int Id { get; }
        public double Amount { get; } // can be only positive or negative
        public DateTime Date { get; }
        public AccountType AccountType { get; }
    }
}
