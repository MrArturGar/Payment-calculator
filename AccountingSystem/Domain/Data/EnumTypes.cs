using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Data
{
    public enum LoanStatus
    {
        NEW, //just created
        NORMAL, //without overdue
        IN_OVERDUE, //with overdue
        CLOSED //repaid and closed
    }
    public enum AccountType
    {
        BASE_DEBT,
        INTEREST,
        PREPAID_BASE_DEBT,
        PREPAID_INTEREST,
        OVERDUE_BASE_DEBT,
        OVERDUE_INTEREST,
        PENALTY
    }
}
