using Microsoft.AspNetCore.Mvc;

namespace SmartHotel. PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class swaggerController : ControllerBase
    {
 

        private readonly ILogger<swaggerController> _logger;

        public swaggerController(ILogger<swaggerController> logger)
        {
            _logger = logger;
        }

   
    }
}
