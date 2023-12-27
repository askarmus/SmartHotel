using Service.Shared.Enum;

namespace SmartHotel. PaymentService.Response
{
    public class PaymentResponse
    {
        public PaymentStatus PaymentStatus { get; set; }
        public string TransactionId { get; set; }
    }
}
