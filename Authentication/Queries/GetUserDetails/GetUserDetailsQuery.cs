using MediatR;
using ModularMonolith.User.Contracts;

namespace Authentication.Queries
{
    public class GetUserDetailsQuery : IRequest<UserDto>
    {
        public GetUserDetailsQuery(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; }
    }
}
