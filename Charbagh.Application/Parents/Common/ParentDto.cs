
namespace Charbagh.Application.Parents.Common
{
    public sealed record ParentDto(
        Guid Id ,
        Guid UserId,
        string FirstName,
        string LastName,
        string? Email,
        string NationalIdentificationNumber,
        string? ProfileImageUrl,
        ParentStatus Status
        );
}
