using Charbagh.Application.Users.Common;
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Users.GetById;

public static class UserDetailsMapper
{
    public static UserDetailsDto ToDetailsDto(this User user)
    {
        return new UserDetailsDto(
            user.ToDto(),
            user.Student is null ? null : new UserProfileDetailsDto(
                user.Student.Id, user.Student.StudentCode, (int)user.Student.Status),
            user.Teacher is null ? null : new UserProfileDetailsDto(
                user.Teacher.Id, user.Teacher.PersonnelCode, (int)user.Teacher.Status),
            user.Parent is null ? null : new UserProfileDetailsDto(
                user.Parent.Id, "—", (int)user.Parent.Status),
            user.Staff is null ? null : new UserProfileDetailsDto(
                user.Staff.Id, user.Staff.PersonnelCode, (int)user.Staff.Status),
            user.UserBranches
                .OrderByDescending(x => x.IsDefault)
                .ThenBy(x => x.Branch.Name)
                .Select(x => new UserBranchDetailsDto(
                    x.Id,
                    x.BranchId,
                    x.Branch.Name,
                    x.Branch.Code,
                    x.IsDefault,
                    x.IsActive,
                    x.AssignedAt,
                    x.UserBranchRoles
                        .OrderBy(r => r.Role.DisplayName)
                        .Select(r => new UserRoleDetailsDto(
                            r.RoleId,
                            r.Role.Name,
                            r.Role.DisplayName,
                            r.IsActive,
                            r.AssignedAt))
                        .ToList()))
                .ToList());
    }
}
