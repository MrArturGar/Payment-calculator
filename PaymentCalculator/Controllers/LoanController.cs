using AccountingSystem.Domain.Data;
using AccountingSystem.Domain.Services;
using AccountingSystem.Domain.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;
using PaymentCalculator.Models;
using System.ComponentModel.DataAnnotations;

namespace PaymentCalculator.Controllers
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //I don't understand why double? Don't use double. This is bad.
    //Он хранится в памяти в двоичном виде, т.е. степени двойки. 
    //На помощь приходит Decimal, он хранится в десятичном виде.
    //
    //Но раз уж по заданию у нас double, вот...
    [ApiController]
    [Route("api/loans")]
    public class LoanController : Controller
    {
        private ILoansHub _loansHub;

        public LoanController(ILoansHub repository)
        {
            _loansHub = repository;
        }

        [HttpGet("{loanId}/full-payment")]
        public ActionResult<FullPayment> GetFullPayment(
            [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")] int loanId)
        {
            var loan = _loansHub.GetLoanById(loanId);
            if (loan == null)
            {
                return NotFound();
            }

            double baseDebt = 0;
            double interest = 0;
            double penalty = 0;

            if (loan.Status == LoanStatus.CLOSED)
            {
                baseDebt = 0;
                interest = 0;
                penalty = 0;
            }
            else
            {
                List<PlannedPayment> paymentSchedule = loan.GetPaymentSchedule();

                DateTime currentDate = DateTime.Now.Date;

                double totalPayment = 0;
                // собираем все счета
                foreach (PlannedPayment payment in paymentSchedule)
                {
                    if (payment.PaymentDate <= currentDate)
                    {
                        baseDebt += payment.BaseDebt;
                        interest += payment.Interest;
                        totalPayment += payment.BaseDebt + payment.Interest;
                    }
                    else
                    {
                        break;
                    }
                }

                //собераем счета при просрочке
                if (loan.Status == LoanStatus.IN_OVERDUE)
                {
                    var overdueOperations = loan.GetOperations()
                        .Where(op => op.AccountType == AccountType.OVERDUE_BASE_DEBT || op.AccountType == AccountType.OVERDUE_INTEREST || op.AccountType == AccountType.PENALTY)
                        .ToList();

                    foreach (IOperation operation in overdueOperations)
                    {
                        if (operation.Date <= currentDate)
                        {
                            if (operation.AccountType == AccountType.OVERDUE_BASE_DEBT)
                            {
                                baseDebt += operation.Amount;
                            }
                            else if (operation.AccountType == AccountType.OVERDUE_INTEREST)
                            {
                                interest += operation.Amount;
                            }
                            else if (operation.AccountType == AccountType.PENALTY)
                            {
                                penalty += operation.Amount;
                            }
                        }
                    }
                }
            }

            var paymentBreakdown = new FullPayment()
            {
                BaseDebt = Math.Round(baseDebt, 2),
                Interest = Math.Round(interest, 2),
                Penalty = Math.Round(penalty, 2),
                Total = Math.Round(baseDebt + interest + penalty, 2)
            };

            return Ok(paymentBreakdown);
        }

        [HttpGet("{loanId}/partial-payment")]
        public ActionResult<PartialPayment> GetPartialPayment(
            [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")] int loanId)
        {
            var loan = _loansHub.GetLoanById(loanId);

            if (loan == null)
            {
                return NotFound();
            }

            var plannedPayments = loan.GetPaymentSchedule();
            var operations = loan.GetOperations();


            // получаем дату ближайшего платежа
            var nextPaymentDate = plannedPayments.Where(x => x.PaymentDate >= DateTime.Today)
                                                           .Select(x => x.PaymentDate)
                                                           .FirstOrDefault();

            // если платежей больше нет, то считаем, что все задолженности уже погашены
            if (nextPaymentDate == default(DateTime))
            {
                return Ok(new PartialPayment { Amount = 0 });
            }

            // проверяем, был ли уже сделан платеж за этот месяц
            var lastPaymentDate = operations.Where(x => x.AccountType == AccountType.BASE_DEBT && x.Amount < 0)
                                                       .Select(x => x.Date)
                                                       .LastOrDefault();

            if (lastPaymentDate >= new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1))
            {
                // если уже был платеж за этот месяц, то ничего не возвращаем
                return Ok(new PartialPayment { Amount = 0 });
            }

            // получаем сумму задолженности до ближайшего платежа
            var remainingBalance = plannedPayments.FirstOrDefault(x => x.PaymentDate == nextPaymentDate)?.RemainingBaseDebt ?? 0;

            // получаем дату следующего дня платежа
            var nextPaymentEndDate = nextPaymentDate.AddDays(1);

            var interestAmount = plannedPayments.Where(x => x.PaymentDate >= DateTime.Today && x.PaymentDate < nextPaymentEndDate)
                                                          .Sum(x => x.Interest);

            var amount = remainingBalance + interestAmount;

            var baseDebtAmount = Math.Min(amount, loan.Amount);
            var interestDebtAmount = amount - baseDebtAmount;


            return Ok(new PartialPayment
            {
                Amount = Math.Round(amount, 2),
                Details = new List<PartialPaymentDetail>
        {
            new PartialPaymentDetail { AccountType = AccountType.PREPAID_BASE_DEBT, Amount = Math.Round(baseDebtAmount, 2) },
            new PartialPaymentDetail { AccountType = AccountType.PREPAID_INTEREST, Amount = Math.Round(interestDebtAmount, 2) }
        }
            });
        }
    }

}
