using AccountingSystem.Domain.Data;
using AccountingSystem.Domain.Services;
using AccountingSystem.Domain.Services.DataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Tests
{
    internal class LoanExamples
    {

        internal ILoansHub GetTestClosedLoan()
        {
            // Arrange
            var loanMock = new Mock<ILoan>();
            loanMock.SetupGet(x => x.Amount).Returns(1000);
            loanMock.SetupGet(x => x.InterestRate).Returns(0.1);
            loanMock.SetupGet(x => x.StartDate).Returns(new DateTime(2022, 1, 1));
            loanMock.SetupGet(x => x.EndDate).Returns(new DateTime(2022, 12, 31));
            loanMock.SetupGet(x => x.CloseDate).Returns(new DateTime(2022, 12, 31));
            loanMock.SetupGet(x => x.Status).Returns(LoanStatus.CLOSED);

            var plannedPayments = new List<PlannedPayment>()
    {
        new PlannedPayment ( new DateTime(2022, 2, 1), 100, 10, 900 ),
        new PlannedPayment (  new DateTime(2022, 3, 1), 100, 9, 800 ),
        new PlannedPayment (  new DateTime(2022, 4, 1), 100, 8, 700 ),
        new PlannedPayment (  new DateTime(2022, 5, 1), 100, 7, 600 ),
        new PlannedPayment (  new DateTime(2022, 6, 1), 100, 6, 500 ),
        new PlannedPayment (  new DateTime(2022, 7, 1), 100, 5, 400 ),
        new PlannedPayment (  new DateTime(2022, 8, 1), 100, 4, 300 ),
        new PlannedPayment (  new DateTime(2022, 9, 1), 100, 3, 200 ),
        new PlannedPayment (  new DateTime(2022, 10, 1), 100, 2, 100 ),
        new PlannedPayment (  new DateTime(2022, 11, 1), 100, 1, 0 ),
    };

            var operations = new List<IOperation>()
            {
                new Operation(1, 100, new DateTime(2022, 2, 1), AccountType.BASE_DEBT),
            };

            loanMock.Setup(x => x.GetPaymentSchedule()).Returns(plannedPayments);
            loanMock.Setup(x => x.GetOperations()).Returns(operations);

            var loansHubMock = new Mock<ILoansHub>();
            loansHubMock.Setup(x => x.GetLoanById(1)).Returns(loanMock.Object);
            return loansHubMock.Object;
        }



        internal ILoansHub GetTestNormalLoan()
        {
            // Arrange
            var loanMock = new Mock<ILoan>();
            loanMock.SetupGet(x => x.Amount).Returns(1000);
            loanMock.SetupGet(x => x.InterestRate).Returns(0.1);
            loanMock.SetupGet(x => x.StartDate).Returns(new DateTime(2023, 1, 1));
            loanMock.SetupGet(x => x.EndDate).Returns(new DateTime(2023, 12, 31));
            loanMock.SetupGet(x => x.CloseDate).Returns(new DateTime(2023, 12, 31));
            loanMock.SetupGet(x => x.Status).Returns(LoanStatus.NORMAL);

            var plannedPayments = new List<PlannedPayment>()
    {
        new PlannedPayment ( new DateTime(2023, 2, 1), 100, 10, 900 ),
        new PlannedPayment (  new DateTime(2023, 3, 1), 100, 9, 800 ),
        new PlannedPayment (  new DateTime(2023, 4, 1), 100, 8, 700 ),
        new PlannedPayment (  new DateTime(2023, 5, 1), 100, 7, 600 ),
        new PlannedPayment (  new DateTime(2023, 6, 1), 100, 6, 500 ),
        new PlannedPayment (  new DateTime(2023, 7, 1), 100, 5, 400 ),
        new PlannedPayment (  new DateTime(2023, 8, 1), 100, 4, 300 ),
        new PlannedPayment (  new DateTime(2023, 9, 1), 100, 3, 200 ),
        new PlannedPayment (  new DateTime(2023, 10, 1), 100, 2, 100 ),
        new PlannedPayment (  new DateTime(2023, 11, 1), 100, 1, 0 ),
    };
            var operations = new List<IOperation>()
            {
                new Operation(1, 100, new DateTime(2023, 2, 1), AccountType.BASE_DEBT),
                new Operation(1, 100, new DateTime(2023, 3, 1), AccountType.BASE_DEBT),
            };
            loanMock.Setup(x => x.GetPaymentSchedule()).Returns(plannedPayments);
            loanMock.Setup(x => x.GetOperations()).Returns(operations);

            var loansHubMock = new Mock<ILoansHub>();
            loansHubMock.Setup(x => x.GetLoanById(1)).Returns(loanMock.Object);
            return loansHubMock.Object;
        }


        internal ILoansHub GetTestInOverdueLoan()
        {
            // Arrange
            var loanMock = new Mock<ILoan>();
            loanMock.SetupGet(x => x.Amount).Returns(1000);
            loanMock.SetupGet(x => x.InterestRate).Returns(0.1);
            loanMock.SetupGet(x => x.StartDate).Returns(new DateTime(2023, 1, 1));
            loanMock.SetupGet(x => x.EndDate).Returns(new DateTime(2023, 12, 31));
            loanMock.SetupGet(x => x.CloseDate).Returns(new DateTime(2023, 12, 31));
            loanMock.SetupGet(x => x.Status).Returns(LoanStatus.IN_OVERDUE);

            var plannedPayments = new List<PlannedPayment>()
    {
        new PlannedPayment ( new DateTime(2023, 2, 1), 100, 10, 900 ),
        new PlannedPayment (  new DateTime(2023, 3, 1), 100, 9, 800 ),
        new PlannedPayment (  new DateTime(2023, 4, 1), 100, 8, 700 ),
        new PlannedPayment (  new DateTime(2023, 5, 1), 100, 7, 600 ),
        new PlannedPayment (  new DateTime(2023, 6, 1), 100, 6, 500 ),
        new PlannedPayment (  new DateTime(2023, 7, 1), 100, 5, 400 ),
        new PlannedPayment (  new DateTime(2023, 8, 1), 100, 4, 300 ),
        new PlannedPayment (  new DateTime(2023, 9, 1), 100, 3, 200 ),
        new PlannedPayment (  new DateTime(2023, 10, 1), 100, 2, 100 ),
        new PlannedPayment (  new DateTime(2023, 11, 1), 100, 1, 0 ),
    };
            var operations = new List<IOperation>()
            {
                new Operation(1, 100, new DateTime(2023, 2, 1), AccountType.BASE_DEBT),
                new Operation(1, 10, new DateTime(2023, 3, 1), AccountType.OVERDUE_INTEREST),
                new Operation(1, 10, new DateTime(2023, 3, 1), AccountType.PENALTY),
            };
            loanMock.Setup(x => x.GetPaymentSchedule()).Returns(plannedPayments);
            loanMock.Setup(x => x.GetOperations()).Returns(operations);

            var loansHubMock = new Mock<ILoansHub>();
            loansHubMock.Setup(x => x.GetLoanById(1)).Returns(loanMock.Object);
            return loansHubMock.Object;
        }
    }
}
