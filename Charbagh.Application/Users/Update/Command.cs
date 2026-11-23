

using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.Update
{
    public sealed record UpdateUserCommand(
     Guid Id,
     string FirstName,
     string LastName,
     string Mobile,
     string? Email,
     Guid NationalityId,
     string NationalIdentificationNumber,
     DateOnly? BirthDate,
     Gender Gender,
     string? Address,
     string? Bio,
     string? ProfileImageUrl
     ) : IRequest<UserDto>;
}
