namespace Charbagh.Application.Authentication.CreateUser;

public sealed record CreateUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Mobile,
    Guid NationalityId,
    string NationalIdentificationNumber,
    string FullName);