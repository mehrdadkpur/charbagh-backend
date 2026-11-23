using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.GetAll
{
    public sealed record GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>;

}
