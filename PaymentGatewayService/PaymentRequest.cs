namespace PaymentGatewayService
{
    public class PaymentRequest
    {
        public string CreditCardNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}

