using AccountingSystem.Domain.Data;

namespace PaymentCalculator.Models
{
    public class PartialPayment
    {
        public double Amount { get; set; } = 0;
        public List<PartialPaymentDetail> Details { get; set; }
    }

    public class PartialPaymentDetail
    {
        public AccountType AccountType { get; set; }
        public double Amount { get; set; }= 0;
    }
    public class PartialPaymentResult
    {
        public double Amount { get; set; }
        public Dictionary<AccountType, double> Breakdown { get; set; }
    }
}