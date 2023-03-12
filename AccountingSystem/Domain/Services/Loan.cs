using AccountingSystem.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Services
{
    internal class Loan : ILoan
    {
        private List<IOperation> _operations;
        private List<PlannedPayment> _payments;

        public double Amount { get; }

        public double InterestRate { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public DateTime CloseDate { get; }

        public LoanStatus Status { get; }

        private Loan(double amount, double interestRate)
        {
            Amount = amount;
            InterestRate = interestRate;
            StartDate = DateTime.Now.Date;
            EndDate = StartDate;
            Status = LoanStatus.NEW;
        }

        public List<IOperation> GetOperations()
        {
             return _operations;
        }

        public List<PlannedPayment> GetPaymentSchedule()
        {
            return _payments;
        }
    }
}
