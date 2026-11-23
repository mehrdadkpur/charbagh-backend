
using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Staffs.Common
{
    public static class SatffMapper
    {
        public static StaffDto ToDto(this Staff staff)
        {
            ArgumentNullException.ThrowIfNull(staff);

            var user = staff.User
                ?? throw new InvalidOperationException(
                    $"User navigation property is null for Staff: {staff.Id}");
            return new StaffDto( 
                staff.Id,
                staff.UserId,
                user.FirstName, 
                user.LastName,
                user.NationalIdentificationNumber,
                user.Email,
                user.ProfileImageUrl,
                staff.PersonnelCode , 
                staff.EmploymentDate , 
                staff.TerminationDate , 
                staff.EmploymentType,
                staff.Status,
                user.Bio
                );
        }
    }
}
