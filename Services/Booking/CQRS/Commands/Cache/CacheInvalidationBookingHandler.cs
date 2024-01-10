using Microsoft.Extensions.Caching.Distributed;

namespace SmartHotel.BookingService.CQRS.Commands.Cache;

internal class CacheInvalidationBookingHandler : INotificationHandler<BookingCreatedEvent>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger _logger;
    public CacheInvalidationBookingHandler(IDistributedCache cache, ILogger<BookingCreatedEvent> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    async Task INotificationHandler<BookingCreatedEvent>.Handle(BookingCreatedEvent notification, CancellationToken cancellationToken)
    {
        await HandleInternal(cancellationToken);
    }

    private async Task HandleInternal(CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("booking-list", cancellationToken);

        _logger.LogInformation("Booking list cache removed");
    }
}
