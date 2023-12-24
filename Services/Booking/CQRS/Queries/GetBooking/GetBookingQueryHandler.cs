using BookingService.CQRS.Queries.GetBooking.Response;
using BookingService.Repository;
using MediatR;
using SmartTicket.Exceptions.Abstraction;

namespace BookingService.CQRS.Queries.GetBooking
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, GetBookingQueryResponse>
    {
        private readonly IBookingRepository _repository;

        public GetBookingQueryHandler(IBookingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<GetBookingQueryResponse> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var booking =  await _repository.GetBookingAsync(request.BookingId);
            if ( booking is null)
                throw new NotFoundException(request.BookingId.ToString(), nameof(Entities.Booking));

            return new GetBookingQueryResponse(booking.BookingId, booking.UserId , booking.RoomId, booking.CheckInDate, booking.CheckOutDate, booking.BookingStatus);

        }
    }
}