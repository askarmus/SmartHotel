namespace SmartHotel.PaymentService.Request;

public class PaymentRequest
{
    public string CreditCardNumber { get; set; }
    public decimal Amount { get; set; }
}

