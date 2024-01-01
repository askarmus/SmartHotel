using MassTransit;
using SmartHotel.PaymentService.Response;
using Polly;
using Serilog;
using Service.Shared;
using Service.Shared.Enum;
using SmartHotel.Infrastructure.Utility;

namespace BookingService.Consumers
{
    public class BookingCreatedConsumer : IConsumer<BookingCreatedEvent>
    {
        public async Task Consume(ConsumeContext<BookingCreatedEvent> context)
        {
            string apiUrl = "http://localhost:5245/api/payment/process";

            try
            {
                var httpClientService = new HttpClientService();
                var requestBody = new { context.Message.CreditCardNumber,  context.Message.Amount };

                var result = await Policy
                    .Handle<HttpRequestException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                    .ExecuteAsync(async () => await httpClientService.SendRequestAsync<PaymentResponse>(apiUrl, HttpMethod.Post, requestBody));

                await context.Publish(new BookingStatusUpdateEvent()
                {
                    BookingId = context.Message.BookingId,
                    PaymentStatus = result.PaymentStatus,
                });
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "Error calling external payment gateway");

                await context.Publish(new BookingStatusUpdateEvent()
                {
                    BookingId = context.Message.BookingId,
                    PaymentStatus = PaymentStatus.Declined
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred");

                await context.Publish(new BookingStatusUpdateEvent()
                {
                    BookingId = context.Message.BookingId,
                    PaymentStatus = PaymentStatus.Error
                });
            }
        }
    }

}
