using SmartHotel.BookingService.CQRS.Queries.GetBooking.Response;
using SmartHotel.BookingService.Repository;
using MediatR;
using SmartHotel.Exceptions.Abstraction;
using Data.Entities;
using SmartHotel.Infrastructure.Behaviors;

namespace SmartHotel.BookingService.CQRS.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<GetBookingQueryResponse>, ICachableQuery
    {
        public int BookingId { get; set; }
        public bool BypassCache { get; set; }
        public string CacheKey => $"booking-{BookingId}";
        public TimeSpan? SlidingExpiration { get; set; }
    }

    internal class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, GetBookingQueryResponse>
    {
        private readonly IBookingRepository _repository;

        public GetBookingQueryHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetBookingQueryResponse> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var booking =  await _repository.GetBookingAsync(request.BookingId);
            if ( booking is null)
                throw new NotFoundException(request.BookingId.ToString(), nameof(Booking));

            return new GetBookingQueryResponse(booking.Id , booking.RoomId, booking.BookingDate, booking.PaymentStatus.ToString());

        }
    }
}