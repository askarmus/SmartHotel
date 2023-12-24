using BookingService.Dto;
using BookingService.Repository;
using MediatR;

namespace BookingService.CQRS.Queries.Handlers
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Entities.Booking>
    {
        private readonly IBookingRepository _repository;

        public GetBookingQueryHandler(IBookingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Entities.Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBookingAsync(request.BookingId);
        }
    }
}
