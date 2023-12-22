using MassTransit;
using Service.Shared;
using Service.Shared.Enum;

namespace BookingService.Consumers
{
    public class BookingCreatedConsumer : IConsumer<BookingCreatedEvent>
    {


        public async Task Consume(ConsumeContext<BookingCreatedEvent> context)
        {
            
        }
    }

}
