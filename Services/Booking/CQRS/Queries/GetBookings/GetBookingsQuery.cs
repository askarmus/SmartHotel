using SmartHotel.BookingService.Repository;
using MediatR;
using SmartHotel.BookingService.CQRS.Queries.GetBookings.Response;
using SmartHotel.Abstraction;
using SmartHotel.Abstraction.Result;
using System.Collections.Generic;

namespace SmartHotel.BookingService.CQRS.Queries.GetBookings
{
    public class GetBookingsQuery : IRequest<Outcome<List<GetBookingsQueryResponse>>>, ICachableQuery
    {
        public bool BypassCache { get; set; }
        public string CacheKey => $"booking-list";
        public TimeSpan? SlidingExpiration { get; set; }
    }

    internal class GetBookingListQueryHandler : IRequestHandler<GetBookingsQuery, Outcome<List<GetBookingsQueryResponse>>>
    {
        private readonly IBookingRepository _repository;

        public GetBookingListQueryHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Outcome<List<GetBookingsQueryResponse>>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _repository.GetBookingsAsync();

            var getBookingQueryResponses = bookings.Select(booking => new GetBookingsQueryResponse
            {
                BookingId = booking.Id,
                RoomId = booking.RoomId,
                BookingDate = booking.BookingDate,
                PaymentStatus = booking.PaymentStatus.ToString()
            }).ToList();

            return  Outcome<List<GetBookingsQueryResponse>>.Success(getBookingQueryResponses);
        }

    }
}