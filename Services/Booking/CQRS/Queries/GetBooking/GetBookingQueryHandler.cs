using SmartHotel.BookingService.CQRS.Queries.GetBooking.Response;
using SmartHotel.BookingService.Repository;
using MediatR;
using SmartHotel.Exceptions.Abstraction;

namespace SmartHotel.BookingService.CQRS.Queries.GetBooking
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

            return new GetBookingQueryResponse(booking.Id , booking.RoomId, booking.BookingDate, booking.PaymentStatus);

        }
    }
}