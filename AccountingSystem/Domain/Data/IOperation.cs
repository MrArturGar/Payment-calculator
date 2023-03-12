using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Data
{
    public interface IOperation
    {
        int Id { get; }
        double Amount { get; } // can be only positive or negative
        DateTime Date { get; }
        AccountType AccountType { get; }
    }
}
