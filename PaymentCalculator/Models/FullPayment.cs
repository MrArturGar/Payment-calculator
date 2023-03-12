namespace PaymentCalculator.Models
{
    public class FullPayment
    {
        public double BaseDebt { get; set; } = 0;
        public double Interest { get; set; } = 0;
        public double Penalty { get; set; } = 0; 
        public double Total { get; set; } = 0;
    }
}