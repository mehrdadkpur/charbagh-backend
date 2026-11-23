using Charbagh.Application.Authentication.CreateUser;
using MediatR;

namespace Charbagh.Application.Users.Create;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Mobile,
    string? Email,
    Guid NationalityId,
    string NationalIdentificationNumber,
    Gender Gender,
    DateOnly? BirthDate,
    string? Address,
    string? Bio
) : IRequest<CreateUserResponse>;