using Asp.Versioning;
using SmartHotel.BookingService;

public static class BookingEndpoints
{
    public static void MapBookingEndpoints ( this IEndpointRouteBuilder app )
    {
        var apiVersionSet = app.NewApiVersionSet().HasDeprecatedApiVersion(new ApiVersion(1)).HasApiVersion(new ApiVersion(2)).ReportApiVersions().Build();

        app.MapPost("api/booking/CreateBooking", CreateBooking).RequireAuthorization();
        app.MapGet("api/v{version:apiVersion}/booking/GetBookings", GetBookingsv1).RequireAuthorization().WithApiVersionSet(apiVersionSet).MapToApiVersion(new ApiVersion(1, 0));
        app.MapGet("api/v{version:apiVersion}/booking/GetBookings", GetBookingsv2).RequireAuthorization().WithApiVersionSet(apiVersionSet).MapToApiVersion(new ApiVersion(2, 0));
    }

    public static async Task<IResult> CreateBooking(CreateBookingRequest _request, IMediator _mediator,  IMapper _mapper, IPublishEndpoint _publishEndpoint, IRequestClient<AvailabilityUpdatedEvent> _client)
    {
        var response = await _client.GetResponse<AvailabilityUpdateResult>(new { _request.RoomId, _request.BookingDate });

        if (response.Message.AvailabilityStatus != Service.Shared.Enum.AvailabilityStatus.AlreadyBooked)
        {
            var bookingCommand = _mapper.Map<CreateBookingCommand>(_request);
            var result = await _mediator.Send(bookingCommand);
            var bookingCreatedEvent = _mapper.Map<BookingCreatedEvent>(_request);

            await _publishEndpoint.Publish(bookingCreatedEvent);

            return Results.Ok(result);
        }
        else
        {
            return Results.BadRequest(Result<Error>.Failure(BookingErrors.Unavailable(_request.BookingDate)));
        }
    }

    public static async Task<IResult> GetBookingsv1(IMediator _mediator)
    {
        var result = await _mediator.Send(new GetBookingsQuery { });

        return Results.Ok(result);
    }

    public static async Task<IResult> GetBookingsv2(IMediator _mediator)
    {
        var result = await _mediator.Send(new GetBookingsQuery { });

        return Results.Ok(result);
    }

}