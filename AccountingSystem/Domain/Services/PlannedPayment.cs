using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Domain.Data;

namespace AccountingSystem.Domain.Services
{
    public class PlannedPayment : IPlannedPayment
    {
        public PlannedPayment(DateTime paymentDate, double baseDebt, double interest, double remainingBaseDebt)
        {
            PaymentDate = paymentDate;
            BaseDebt = baseDebt;
            Interest = interest;
            RemainingBaseDebt = remainingBaseDebt;
        }

        public DateTime PaymentDate { get; }
        public double BaseDebt { get; }
        public double Interest { get; }
        public double RemainingBaseDebt { get; }
    }

}
