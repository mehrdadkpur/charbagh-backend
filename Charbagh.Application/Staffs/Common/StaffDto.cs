
namespace Charbagh.Application.Staffs.Common
{
    public sealed record StaffDto(
        Guid Id ,
        Guid UserId,
        string FirstName,
        string LastName,
        string NationalIdentificationNumber,
        string? Email,
        string? ProfileImageUrl,
        string PersonnelCode , 
        DateOnly EmploymentDate , 
        DateOnly? TerminationDate , 
        EmploymentType EmploymentType , 
        StaffStatus Status,
        string? Bio
        );
}
