using Azure.Core;
using SmartTicket.Exceptions.Abstraction;

namespace Authentication.Exceptions
{
    public class RoomAvailabilityException : AppException
    {

        public RoomAvailabilityException(int roomId) : base($"RoomAvailability with ID {roomId} not found.", 102)
        {
        }
    }
}
