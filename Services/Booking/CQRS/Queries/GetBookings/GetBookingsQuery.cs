using MediatR;
using SmartHotel.BookingService.CQRS.Queries.GetBookings.Response;
using SmartHotel.Abstraction;
using SmartHotel.Abstraction.Result;
using System.Collections.Generic;
using Persistance.Repository;

namespace SmartHotel.BookingService.CQRS.Queries.GetBookings
{
    public class GetBookingsQuery : IRequest<Result<List<GetBookingsQueryResponse>>>, ICachableQuery
    {
        public bool BypassCache { get; set; }
        public string CacheKey => $"booking-list";
        public TimeSpan? SlidingExpiration { get; set; }
    }

    internal class GetBookingListQueryHandler : IRequestHandler<GetBookingsQuery, Result<List<GetBookingsQueryResponse>>>
    {
        private readonly IBookingRepository _repository;

        public GetBookingListQueryHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetBookingsQueryResponse>>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _repository.GetBookingsAsync();

            var getBookingQueryResponses = bookings.Select(booking => new GetBookingsQueryResponse
            {
                BookingId = booking.Id,
                RoomId = booking.RoomId,
                BookingDate = booking.BookingDate,
                PaymentStatus = booking.PaymentStatus.ToString()
            }).ToList();

            return  Result<List<GetBookingsQueryResponse>>.Success(getBookingQueryResponses);
        }

    }
}