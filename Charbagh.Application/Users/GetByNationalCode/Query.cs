using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.GetByNationalCode
{
    public sealed record GetUserByNationalCodeQuery(string NationalCode) : IRequest<UserDto>;
  
}
