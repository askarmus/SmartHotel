using Microsoft.AspNetCore.Mvc;

namespace FakePaymentGatewayService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        public enum PaymentStatus
        {
            Success,
            Declined
        }

        [HttpPost("Process")]
        public async Task<IActionResult> Process([FromBody] PaymentRequest request)
        {
            await Task.Delay(1000);

            int lastDigit = int.Parse(request.CreditCardNumber.Substring(request.CreditCardNumber.Length - 1));

            if (lastDigit <= 6)
            {
                return Ok(new PaymentResponse
                {
                    PaymentStatus =  PaymentStatus.Success,
                    TransactionId = Guid.NewGuid().ToString(),
                });
            } 

            return BadRequest(new PaymentResponse
            {
                PaymentStatus = PaymentStatus.Declined,
                TransactionId = string.Empty,
            });
        }
    }
}
