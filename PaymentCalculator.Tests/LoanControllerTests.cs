using AccountingSystem.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using PaymentCalculator.Controllers;
using PaymentCalculator.Models;
using Xunit;

namespace PaymentCalculator.Tests
{
    public class LoanControllerTests
    {
        [Fact]
        public void GetFullPaymentIsFound()
        {
            LoanExamples examples = new LoanExamples();
            LoanController controller = new LoanController(examples.GetTestClosedLoan());


            ActionResult<FullPayment> result = controller.GetFullPayment(1);


            Assert.IsNotType<NotFoundResult>(result);
        }

        [Fact]
        public void GetFullPaymentNormal()
        {
            LoanExamples examples = new LoanExamples();
            LoanController controller = new LoanController(examples.GetTestNormalLoan());


            ActionResult<FullPayment> result = controller.GetFullPayment(1);


            Assert.IsType<OkObjectResult>(result.Result);
            var payment = (result.Result as OkObjectResult).Value as FullPayment;
            Assert.True(payment.Total > 200);
        }

        [Fact]
        public void GetFullPaymentInOverdue()
        {
            LoanExamples examples = new LoanExamples();
            LoanController controller = new LoanController(examples.GetTestInOverdueLoan());


            ActionResult<FullPayment> result = controller.GetFullPayment(1);


            Assert.IsType<OkObjectResult>(result.Result);
            var payment = (result.Result as OkObjectResult).Value as FullPayment;
            Assert.Equal(payment.Penalty, 10);
        }

        [Fact]
        public void GetPartialPaymentIsFound()
        {
            LoanExamples examples = new LoanExamples();
            LoanController controller = new LoanController(examples.GetTestClosedLoan());


            ActionResult<PartialPayment> result = controller.GetPartialPayment(1);


            Assert.IsNotType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPartialPaymentNormal()
        {
            LoanExamples examples = new LoanExamples();
            LoanController controller = new LoanController(examples.GetTestNormalLoan());


            ActionResult<PartialPayment> result = controller.GetPartialPayment(1);


            Assert.IsType<OkObjectResult>(result.Result);
            var payment = (result.Result as OkObjectResult).Value as PartialPayment;
            Assert.NotNull(payment);
            Assert.True(payment.Amount > 700 && payment.Amount < 800);
        }

        [Fact]
        public void GetPartialPaymentInOverdue()
        {
            LoanExamples examples = new LoanExamples();
            LoanController controller = new LoanController(examples.GetTestInOverdueLoan());


            ActionResult<PartialPayment> result = controller.GetPartialPayment(1);


            Assert.IsType<OkObjectResult>(result.Result);
            var payment = (result.Result as OkObjectResult).Value as PartialPayment;
            Assert.NotNull(payment);
        }
    }
}
