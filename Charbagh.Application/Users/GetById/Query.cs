using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.GetById
{
    public sealed record GetUserByIdQuery (Guid Id) : IRequest<UserDto>;
  
}
