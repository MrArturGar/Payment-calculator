using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Domain.Services;

namespace AccountingSystem.Domain.Data
{

    public interface ILoan
    {
        double Amount { get; }
        double InterestRate { get; }
        DateTime StartDate { get; } // date when client took a money
        DateTime EndDate { get; } // assuming date of last payment (by payments schedule)
        DateTime CloseDate { get; } // real date of last payment (!= default only for closed loans)
        LoanStatus Status { get; }
        List<PlannedPayment> GetPaymentSchedule();
        List<IOperation> GetOperations();
    }

}
