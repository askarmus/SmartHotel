using static PaymentGatewayService.Controllers.PaymentController;

namespace PaymentGatewayService
{
    public class PaymentResponse
    {
        public PaymentStatus PaymentStatus { get; set; }
        public string TransactionId { get; set; }
    }
}


