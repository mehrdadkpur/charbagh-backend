using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetById;

public sealed class GetRoleByIdQueryHandler
    : IRequestHandler<GetRoleByIdQuery, RoleDetailsDto>
{
    private readonly IRoleManagementRepository _repository;

    public GetRoleByIdQueryHandler(
        IRoleManagementRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoleDetailsDto> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var role = await _repository.GetDetailsAsync(
            request.Id,
            cancellationToken);

        if (role is null)
            throw new KeyNotFoundException("Role not found.");

        var users = role.UserBranchRoles
            .Select(x => new RoleUserDto(
                x.Id,
                x.UserBranchId,
                x.UserBranch.UserId,
                x.UserBranch.User.FirstName,
                x.UserBranch.User.LastName,
                x.UserBranch.User.Mobile,
                x.UserBranch.User.Email,
                x.UserBranch.BranchId,
                x.UserBranch.Branch.Name,
                x.UserBranch.IsActive,
                x.IsActive,
                x.AssignedAt,
                x.AssignedBy,
                x.AssignedByUser is null
                    ? null
                    : $"{x.AssignedByUser.FirstName} {x.AssignedByUser.LastName}"
            ))
            .OrderByDescending(x => x.AssignedAt)
            .ToList();

        var permissions = role.RolePermissions
            .Select(x => x.Permission)
            .Where(x => x is not null)
            .Select(x => new PermissionDto(
                x.Id,
                x.Module,
                x.Name,
                x.Action,
                x.DisplayName,
                x.Description,
                x.IsSystem
            ))
            .OrderBy(x => x.Module)
            .ThenBy(x => x.Action)
            .ToList();

        var activeAssignments = users
            .Where(x => x.IsAssignmentActive && x.IsUserBranchActive)
            .ToList();

        return new RoleDetailsDto(
            role.Id,
            role.Name,
            role.DisplayName,
            role.Description,
            role.IsSystem,
            role.IsActive,
            role.CreatedAt,
            role.UpdatedAt,
            activeAssignments.Count,
            activeAssignments
                .Select(x => x.UserId)
                .Distinct()
                .Count(),
            activeAssignments
                .Select(x => x.BranchId)
                .Distinct()
                .Count(),
            users,
            permissions
        );
    }
}