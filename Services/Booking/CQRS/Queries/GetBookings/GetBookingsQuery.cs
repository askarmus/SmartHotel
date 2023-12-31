using SmartHotel.BookingService.Repository;
using MediatR;
using SmartHotel.Exceptions.Abstraction;
using SmartHotel.BookingService.CQRS.Queries.GetBookings.Response;

namespace SmartHotel.BookingService.CQRS.Queries.GetBookings
{
    public class GetBookingsQuery : IRequest<List<GetBookingsQueryResponse>>, ICachableQuery
    {
        public bool BypassCache { get; set; }
        public string CacheKey => $"booking-list";
        public TimeSpan? SlidingExpiration { get; set; }
    }

    internal class GetBookingListQueryHandler : IRequestHandler<GetBookingsQuery, List<GetBookingsQueryResponse>>
    {
        private readonly IBookingRepository _repository;

        public GetBookingListQueryHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetBookingsQueryResponse>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _repository.GetBookingsAsync();

            var getBookingQueryResponses = bookings.Select(booking => new GetBookingsQueryResponse
            {
                BookingId = booking.Id,
                RoomId = booking.RoomId,
                BookingDate = booking.BookingDate,
                PaymentStatus = booking.PaymentStatus.ToString()
            }).ToList();

            return getBookingQueryResponses;
        }

    }
}