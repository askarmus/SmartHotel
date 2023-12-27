using static FakePaymentGatewayService.Controllers.PaymentController;

namespace FakePaymentGatewayService
{
    public class PaymentResponse
    {
        public PaymentStatus PaymentStatus { get; set; }
        public string TransactionId { get; set; }
    }
}


