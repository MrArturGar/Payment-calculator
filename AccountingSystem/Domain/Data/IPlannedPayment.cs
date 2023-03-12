using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Data
{
    public interface IPlannedPayment
    {
        DateTime PaymentDate { get; }
        double BaseDebt { get; }
        double Interest { get; }
        double RemainingBaseDebt { get; }
    }

}
